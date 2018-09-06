using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Task.Data;
using Task.Entity;

namespace Task.Business
{
  public class TaskManager
  {
        //private TaskManagerContext tmDB = new TaskManagerContext();
        private TaskManagerContext tmDB;
        public TaskManager(TaskManagerContext context)
        {
            tmDB = context;
        }

    public IQueryable<TaskDTO> FindAll()
    {
      var taskDTO = from b in this.tmDB.TaskDetail
                    select new TaskDTO()
                    {
                      TaskID = b.TaskID,
                      TaskName = b.TaskName == null ? "" : b.TaskName,
                      StartDate = b.StartDate,
                      EndDate = b.EndDate,
                      IsActive = b.IsActive,
                      ParentTaskName = b.ParentTask == null ? "" : b.ParentTask.ParentTaskName,
                      Priority = b.Priority
                    };

      return taskDTO;
    }

    public TaskDTO Find(int id)
    {

      TaskDetail task = new TaskDetail();
      task = this.tmDB.TaskDetail.FirstOrDefault(x => x.TaskID == id);
      TaskDTO tskDTO = new TaskDTO();

      tskDTO.TaskID = task.TaskID;
      tskDTO.TaskName = task.TaskName;
      tskDTO.StartDate = task.StartDate.Date;
      tskDTO.EndDate = task.EndDate.Date;
      tskDTO.IsActive = task.IsActive;
      tskDTO.ParentTaskName = task.ParentTask == null ? "" : task.ParentTask.ParentTaskName;
      tskDTO.Priority = task.Priority;

      return tskDTO;

    }

    public int SaveTask(TaskDTO taskDTO)
    {
      if (taskDTO.TaskID > 0)
      {
        var task = this.tmDB.TaskDetail.FirstOrDefault(x => x.TaskID == taskDTO.TaskID);


        if (taskDTO.ParentTaskName != "")
        {
          var prntTask = this.tmDB.ParentTask.FirstOrDefault(x => x.ParentTaskName == taskDTO.ParentTaskName);
          if (prntTask != null)
          {
            task.ParentTaskID = prntTask.ParentTaskID;
          }
          else
          {
            ParentTask parentTaskNew = new ParentTask();
            parentTaskNew.ParentTaskName = taskDTO.ParentTaskName;
            this.tmDB.ParentTask.Add(parentTaskNew);
            this.tmDB.SaveChanges();

            prntTask = this.tmDB.ParentTask.FirstOrDefault(x => x.ParentTaskName == taskDTO.ParentTaskName);
            task.ParentTaskID = prntTask.ParentTaskID;
          }
        }

        task.TaskName = taskDTO.TaskName;
        task.StartDate = taskDTO.StartDate;
        task.Priority = taskDTO.Priority;
        task.EndDate = taskDTO.EndDate;

        //this.taskManager.TaskDetail.Add(task);
        this.tmDB.SaveChanges();

        //dbCustomer.Address = customer.Address;
        //dbCustomer.Phone = customer.Phone;
        //db.SaveChanges();

        return task.TaskID;
      }
      else
      {
        var task = new TaskDetail();

        task.TaskName = taskDTO.TaskName;
        task.StartDate = taskDTO.StartDate;
        task.Priority = taskDTO.Priority;
        task.EndDate = taskDTO.EndDate;

        if (taskDTO.ParentTaskName != "")
        {
          var prntTask = this.tmDB.ParentTask.FirstOrDefault(x => x.ParentTaskName == taskDTO.ParentTaskName);
          if (prntTask != null)
          {
            task.ParentTaskID = prntTask.ParentTaskID;
          }
          else
          {
            ParentTask parentTaskNew = new ParentTask();
            parentTaskNew.ParentTaskName = taskDTO.ParentTaskName;
            this.tmDB.ParentTask.Add(parentTaskNew);
            this.tmDB.SaveChanges();

            prntTask = this.tmDB.ParentTask.FirstOrDefault(x => x.ParentTaskName == taskDTO.ParentTaskName);
            task.ParentTaskID = prntTask.ParentTaskID;
          }
        }

        this.tmDB.TaskDetail.Add(task);
        this.tmDB.SaveChanges();

        return task.TaskID;
      }
    }

    public int EndTaskByID(int id)
    {

      var task = this.tmDB.TaskDetail.FirstOrDefault(x => x.TaskID == id);

      task.IsActive = "False";
      this.tmDB.SaveChanges();
      return id;
    }

  }
}
