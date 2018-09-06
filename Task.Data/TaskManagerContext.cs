using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Task.Entity;


namespace Task.Data
{
    public class TaskManagerContext : DbContext
    {

        public TaskManagerContext() : base("TaskManagerContext")
        {

        }

        public virtual DbSet<TaskDetail> TaskDetail { get; set; }
        public virtual DbSet<ParentTask> ParentTask { get; set; }
    }
}
