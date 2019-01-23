using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Common.Constant;
using Common.ModelsDTO;
using gtdtimer.Attributes;
using ServiceTier.Services;
using gtdtimer.ActionResults;

namespace gtdtimer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService taskService;
        private readonly IUserIdentityService userIdentityService;

        public TasksController(
            ITaskService taskService,
            IUserIdentityService userIdentityService
            )
        {
            this.taskService = taskService;
            this.userIdentityService = userIdentityService;
        }

        /// <summary>
        /// Returns all users' tasks.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Constants.AdminRole)]
        [HttpGet("[action]")]
        public IActionResult GetAllTasks()
        {
            var allTasks = taskService.GetAllTasks();

            return Ok(allTasks);
        }

        /// <summary>
        /// Returns all users' active tasks.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Constants.AdminRole)]
        [HttpGet("[action]")]
        public IActionResult GetAllActiveTasks()
        {
            var allTasks = taskService.GetAllActiveTasks();

            return Ok(allTasks);
        }

        /// <summary>
        /// Returns all users' archived tasks.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Constants.AdminRole)]
        [HttpGet("[action]")]
        public IActionResult GetAllArchivedTasks()
        {
            var allTasks = taskService.GetAllArchivedTasks();

            return Ok(allTasks);
        }

        /// <summary>
        /// Returns user's task by id.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("GetTaskById/{taskID}")]
        public IActionResult GetTaskById(int taskId)
        {
            TaskDTO task = taskService.GetTaskById(taskId);

            return Ok(task);
        }

        /// <summary>
        /// Returns all user's tasks.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetAllTasksByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var tasks = taskService.GetAllTasksByUserId(userId);

            return Ok(tasks);
        }

        /// <summary>
        /// Returns all user's active tasks.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetAllActiveTasksByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var tasks = taskService.GetAllActiveTasksByUserId(userId);

            return Ok(tasks);
        }

        /// <summary>
        /// Returns all user's archived tasks.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetAllArchivedTasksByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var tasks = taskService.GetAllArchivedTasksByUserId(userId);

            return Ok(tasks);
        }

        /// <summary>
        /// Creates a task.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateModel]
        [HttpPost("[action]")]
        public IActionResult CreateTask([FromBody]TaskDTO model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.CreateTask(model);

            return Ok(model);
        }

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateModel]
        [HttpPut("[action]")]
        public IActionResult UpdateTask([FromBody]TaskDTO model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.UpdateTask(model);

            return Ok();
        }

        /// <summary>
        /// Deletes the task by id.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteTask/{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            taskService.DeleteTaskById(taskId);

            return Ok();
        }

        /// <summary>
        /// Changes IsActive status of the task.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateModel]
        [HttpPut("[action]")]
        public IActionResult SwitchArchivedStatus([FromBody]TaskDTO model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.SwitchArchivedStatus(model);

            return Ok();
        }

        /// <summary>
        /// Sets IsRunning status of the task to true.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateModel]
        [HttpPut("[action]")]
        public IActionResult StartTask([FromBody]TaskDTO model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.StartTask(model);

            return Ok();
        }

        /// <summary>
        /// Sets IsRunning status of the task to false.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateModel]
        [HttpPut("[action]")]
        public IActionResult PauseTask([FromBody]TaskDTO model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.PauseTask(model);

            return Ok();
        }

        /// <summary>
        /// Resets task's properties.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateModel]
        [HttpPut("[action]")]
        public IActionResult ResetTask([FromBody]TaskDTO model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.ResetTask(model);

            return Ok();
        }

        /// <summary>
        /// Converts all user's tasks to xml format. 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllTasksAsXmlByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfTasks = taskService.GetAllTasksByUserId(userId);

            return new XmlResult(listOfTasks);
        }

        /// <summary>
        /// Converts all user's active tasks to xml format.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllActiveTasksAsXmlByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfTasks = taskService.GetAllActiveTasksByUserId(userId);

            return new XmlResult(listOfTasks);
        }

        /// <summary>
        /// Converts all user's archived tasks to xml format.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllArchivedTasksAsXmlByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfTasks = taskService.GetAllArchivedTasksByUserId(userId);

            return new XmlResult(listOfTasks);
        }

        /// <summary>
        /// Converts  user's task by id to xml format.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{taskId}")]
        public IActionResult ExportTaskAsXmlById(int taskId)
        {
            var task = taskService.GetTaskById(taskId);

            return new XmlResult(task);
        }

        /// <summary>
        /// Converts all user's tasks to csv format.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllTasksAsCsvByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfTasks = taskService.GetAllTasksByUserId(userId);

            return new CsvResult(listOfTasks);
        }

        /// <summary>
        /// Converts all user's active tasks to csv format.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllActiveTasksAsCsvByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfTasks = taskService.GetAllActiveTasksByUserId(userId);

            return new CsvResult(listOfTasks);
        }

        /// <summary>
        /// Converts all user's archived tasks to csv format.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllArchivedTasksAsCsvByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfTasks = taskService.GetAllArchivedTasksByUserId(userId);

            return new CsvResult(listOfTasks);
        }

        /// <summary>
        /// Converts  user's task by id to csv format.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{taskId}")]
        public IActionResult ExportTaskAsCsvById(int taskId)
        {
            var task = taskService.GetTaskById(taskId);

            return new CsvResult(task);
        }

        /// <summary>
        /// Imports file of user's tasks in .csv format.
        /// </summary>
        /// <param name="uploadFile"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult ImportTasksAsCsv(IFormFile uploadFile)
        {
            var userId = userIdentityService.GetUserId();
            var listOfTasks = taskService.ImportTasksFromCsv(uploadFile, userId);
            
            return Ok(listOfTasks);
        }

        /// <summary>
        /// Imports file of user's tasks in .xml format.
        /// </summary>
        /// <param name="uploadFile"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult ImportTasksAsXml(IFormFile uploadFile)
        {
            var userId = userIdentityService.GetUserId();
            var listOfTasks = taskService.ImportTasksFromXml(uploadFile, userId);

            return Ok(listOfTasks);
        }
    }
}