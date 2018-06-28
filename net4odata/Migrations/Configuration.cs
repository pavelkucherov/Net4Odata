namespace net4odata.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using courses_odata.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<net4odata.Models.net4odataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(net4odata.Models.net4odataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (context.Courses.Any()) return;

            var r = new Random();

            var courses = new List<Course>
        {
          new Course() {
            Name = "Dev 1",
            Points = 4,
            Position = 1
          },
          new Course() {
            Name = "Dev 2",
            Points = 4,
            Position = 2
          },
          new Course() {
            Name = "Dev 3",
            Points = 4,
            Position = 1
          },
        };
            if (!context.Courses.Any())
            {
                context.Courses.AddRange(courses);
                context.SaveChanges();
            }

            var lectures = new List<Lecture>();
            foreach (var course in courses)
            {
                for (int i = 0; i < r.Next(3, 6); i++)
                {
                    lectures.Add(
                      new Lecture()
                      {
                          Title = $"{course.Name}, Lec {i + 1}",
                          Content = "Blahblah",
                          NotRequired = r.NextDouble() > 0.9,
                          Position = i,
                          CourseId = course.Id,
                      }
                    );
                }
            }

            if (!context.Lectures.Any())
            {
                context.Lectures.AddRange(lectures);
                context.SaveChanges();
            }

            var teaching_activities = new List<TeachingActivity>();
            foreach (var lecture in lectures)
            {
                for (int i = 0; i < r.Next(3, 6); i++)
                {
                    if (r.NextDouble() > 0.5)
                    {
                        teaching_activities.Add(
                          new Slide()
                          {
                              Title = $"{lecture.Title}, Slide {i + 1}",
                              Content = "Blahblah",
                              Position = i,
                              LectureId = lecture.Id,
                          }
                        );
                    }
                    else
                    {
                        teaching_activities.Add(
                          new MultipleChoice()
                          {
                              Title = $"{lecture.Title}, Question {i + 1}",
                              Question = "Blahblah",
                              Position = i,
                              LectureId = lecture.Id,
                          }
                        );
                    }
                }
            }

            if (!context.TeachingActivities.Any())
            {
                context.TeachingActivities.AddRange(teaching_activities);
                context.SaveChanges();
            }

            var answers = new List<Answer>();
            foreach (var mc in teaching_activities.Where(ta => ta is MultipleChoice).Cast<MultipleChoice>())
            {
                for (int i = 0; i < r.Next(3, 5); i++)
                {
                    Console.WriteLine("adding answer");
                    answers.Add(
                      new Answer()
                      {
                          IsCorrect = i == 0,
                          Content = "blah blah answer",
                          MultipleChoiceId = mc.Id
                      }
                    );
                }
            }

            if (!context.Answers.Any())
            {
                context.Answers.AddRange(answers);
                context.SaveChanges();
            }
        }
    }
}
