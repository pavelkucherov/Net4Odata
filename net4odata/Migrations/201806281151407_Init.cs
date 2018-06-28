namespace net4odata.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lectures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        NotRequired = c.Boolean(nullable: false),
                        Position = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Points = c.Int(nullable: false),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeachingActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Position = c.Int(nullable: false),
                        LectureId = c.Int(nullable: false),
                        Question = c.String(),
                        Content = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lectures", t => t.LectureId, cascadeDelete: true)
                .Index(t => t.LectureId);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsCorrect = c.Boolean(nullable: false),
                        Content = c.String(),
                        MultipleChoiceId = c.Int(nullable: false),
                        Slide_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeachingActivities", t => t.MultipleChoiceId, cascadeDelete: true)
                .ForeignKey("dbo.TeachingActivities", t => t.Slide_Id)
                .Index(t => t.MultipleChoiceId)
                .Index(t => t.Slide_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "Slide_Id", "dbo.TeachingActivities");
            DropForeignKey("dbo.Answers", "MultipleChoiceId", "dbo.TeachingActivities");
            DropForeignKey("dbo.TeachingActivities", "LectureId", "dbo.Lectures");
            DropForeignKey("dbo.Lectures", "CourseId", "dbo.Courses");
            DropIndex("dbo.Answers", new[] { "Slide_Id" });
            DropIndex("dbo.Answers", new[] { "MultipleChoiceId" });
            DropIndex("dbo.TeachingActivities", new[] { "LectureId" });
            DropIndex("dbo.Lectures", new[] { "CourseId" });
            DropTable("dbo.Answers");
            DropTable("dbo.TeachingActivities");
            DropTable("dbo.Courses");
            DropTable("dbo.Lectures");
        }
    }
}
