namespace Task.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskDetails", "ParentTaskID", "dbo.ParentTasks");
            DropIndex("dbo.TaskDetails", new[] { "ParentTaskID" });
            AddColumn("dbo.TaskDetails", "Priority", c => c.Int(nullable: false));
            AlterColumn("dbo.TaskDetails", "ParentTaskID", c => c.Int());
            CreateIndex("dbo.TaskDetails", "ParentTaskID");
            AddForeignKey("dbo.TaskDetails", "ParentTaskID", "dbo.ParentTasks", "ParentTaskID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskDetails", "ParentTaskID", "dbo.ParentTasks");
            DropIndex("dbo.TaskDetails", new[] { "ParentTaskID" });
            AlterColumn("dbo.TaskDetails", "ParentTaskID", c => c.Int(nullable: false));
            DropColumn("dbo.TaskDetails", "Priority");
            CreateIndex("dbo.TaskDetails", "ParentTaskID");
            AddForeignKey("dbo.TaskDetails", "ParentTaskID", "dbo.ParentTasks", "ParentTaskID", cascadeDelete: true);
        }
    }
}
