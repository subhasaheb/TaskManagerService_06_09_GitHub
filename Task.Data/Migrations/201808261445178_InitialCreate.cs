namespace Task.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParentTasks",
                c => new
                    {
                        ParentTaskID = c.Int(nullable: false, identity: true),
                        ParentTaskName = c.String(),
                    })
                .PrimaryKey(t => t.ParentTaskID);
            
            CreateTable(
                "dbo.TaskDetails",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        TaskName = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsActive = c.String(),
                        ParentTaskID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskID)
                .ForeignKey("dbo.ParentTasks", t => t.ParentTaskID, cascadeDelete: true)
                .Index(t => t.ParentTaskID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskDetails", "ParentTaskID", "dbo.ParentTasks");
            DropIndex("dbo.TaskDetails", new[] { "ParentTaskID" });
            DropTable("dbo.TaskDetails");
            DropTable("dbo.ParentTasks");
        }
    }
}
