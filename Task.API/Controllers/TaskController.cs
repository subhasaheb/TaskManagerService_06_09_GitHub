using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Task.Business;
using Task.Entity;
using Task.Data;

namespace Task.API.Controllers
{
  public class TaskController : ApiController
  {
        
    private TaskManager taskManager = new TaskManager(new TaskManagerContext());

    [HttpGet]
    [Route("")]
    [Route("api/Task/GetTask")]
    public IHttpActionResult GetTask()
    {

      var taskDTO = taskManager.FindAll();
      int num = taskDTO.Count();
      return Ok(taskDTO);
    }

    [HttpGet]
    [Route("api/Task/GetTaskId/{id}")]
    public IHttpActionResult GetTaskById(int id)
    {
      TaskDTO tskDTO = new TaskDTO();
      tskDTO = taskManager.Find(id);

      if (tskDTO != null)
        return Ok(tskDTO);
      else
        return NotFound();

    }


    [HttpPost]
    [Route("api/Task/SaveTask")]
    public IHttpActionResult SaveTask(TaskDTO taskDTO)
    {
      var id = taskManager.SaveTask(taskDTO);

      return Ok(id);

    }

    [HttpDelete]
    [Route("api/Task/EndTask/{id}")]
    public IHttpActionResult EndTaskById(int id)
    {
      var TaskID = taskManager.EndTaskByID(id);
      return Ok(TaskID);
    }


  }
}
