using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Entity
{
  public class TaskDTO
  {
    public int TaskID { get; set; }
    public string TaskName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string IsActive { get; set; }
    public string ParentTaskName { get; set; }
    public int Priority { get; set; }
  }
}
