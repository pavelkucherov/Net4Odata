using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using courses_odata.Model;

namespace net4odata.Models
{
    public class net4odataContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public net4odataContext() : base("name=net4odataContext")
        {
        }

        public System.Data.Entity.DbSet<courses_odata.Model.TeachingActivity> TeachingActivities { get; set; }

        public System.Data.Entity.DbSet<courses_odata.Model.Lecture> Lectures { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Answer> Answers { get; set; }

        /*
         modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("InstructorID")
                    .ToTable("CourseInstructor"));
         */
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasMany(p => p.Lectures);
            //.WithOne(b => b.Course)
            //.HasForeignKey(b => b.CourseId);

            modelBuilder.Entity<Lecture>()
                .HasMany(p => p.TeachingActivities);
                //.WithOne(b => b.Lecture)
                //.HasForeignKey(b => b.LectureId);

            modelBuilder.Entity<Answer>();

            modelBuilder.Entity<Slide>();

            modelBuilder.Entity<MultipleChoice>()
              .HasMany(p => p.Answers);
              //.WithOne(b => b.MultipleChoice)
              //.HasForeignKey(b => b.MultipleChoiceId);

            modelBuilder.Entity<TeachingActivity>();


        }
    }
}
