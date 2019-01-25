//-----------------------------------------------------------------------
// <copyright file="TasksController.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using GtdCommon.Constant;
using GtdCommon.ModelsDto;
using GtdTimer.Attributes;
using GtdTimer.ActionResults;
using GtdServiceTier.Services;

namespace GtdTimer.Controllers
{
    /// <summary>
    /// class for tasks controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        /// <summary>
        /// instance of task service
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// instance of user identity service
        /// </summary>
        private readonly IUserIdentityService userIdentityService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TasksController" /> class.
        /// </summary>
        /// <param name="taskService">instance of task service</param>
        /// <param name="userIdentityService">instance of user identity service</param>
        public TasksController(ITaskService taskService, IUserIdentityService userIdentityService)
        {
            this.taskService = taskService;
            this.userIdentityService = userIdentityService;
        }

        /// <summary>
        /// Returns all users' tasks.
        /// </summary>
        /// <returns>result of getting all users tasks.</returns>
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
        /// <returns>result of getting all users' active tasks.</returns>
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
        /// <returns>result of getting all users' archived tasks.</returns>
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
        /// <param name="taskId">id of chosen task</param>
        /// <returns>result of getting user's task by id.</returns>
        [HttpGet("GetTaskById/{taskID}")]
        public IActionResult GetTaskById(int taskId)
        {
            TaskDto task = taskService.GetTaskById(taskId);

            return Ok(task);
        }

        /// <summary>
        /// Returns all user's tasks.
        /// </summary>
        /// <returns>result of getting all user's tasks.</returns>
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
        /// <returns>result of getting all user's active tasks.</returns>
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
        /// <returns>result of getting all user's archived tasks.</returns>
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
        /// <param name="model">task model</param>
        /// <returns>result of creating task</returns>
        [ValidateModel]
        [HttpPost("[action]")]
        public IActionResult CreateTask([FromBody]TaskDto model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.CreateTask(model);

            return Ok(model);
        }

        /// <summary>
        /// Updates the task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>result of updating the task.</returns>
        [ValidateModel]
        [HttpPut("[action]")]
        public IActionResult UpdateTask([FromBody]TaskDto model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.UpdateTask(model);

            return Ok();
        }

        /// <summary>
        /// Deletes the task by id.
        /// </summary>
        /// <param name="taskId">id of chosen task</param>
        /// <returns>result of deleting the task by id.</returns>
        [HttpDelete("DeleteTask/{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            taskService.DeleteTaskById(taskId);

            return Ok();
        }

        /// <summary>
        /// Changes IsActive status of the task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>result of changing IsActive status of the task.</returns>
        [ValidateModel]
        [HttpPut("[action]")]
        public IActionResult SwitchArchivedStatus([FromBody]TaskDto model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.SwitchArchivedStatus(model);

            return Ok();
        }

        /// <summary>
        /// Sets IsRunning status of the task to true.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>result of setting IsRunning status of the task to true.</returns>
        [ValidateModel]
        [HttpPut("[action]")]
        public IActionResult StartTask([FromBody]TaskDto model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.StartTask(model);

            return Ok();
        }

        /// <summary>
        /// Sets IsRunning status of the task to false.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>result of setting IsRunning status of the task to false.</returns>
        [ValidateModel]
        [HttpPut("[action]")]
        public IActionResult PauseTask([FromBody]TaskDto model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.PauseTask(model);

            return Ok();
        }

        /// <summary>
        /// Resets task's properties.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>result of resetting task's properties.</returns>
        [ValidateModel]
        [HttpPut("[action]")]
        public IActionResult ResetTask([FromBody]TaskDto model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.ResetTask(model);

            return Ok();
        }

        /// <summary>
        /// Converts all user's tasks to xml format. 
        /// </summary>
        /// <returns>returns a text file</returns>
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
        /// <returns>returns a text file</returns>
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
        /// <returns>returns text file</returns>
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
        /// <param name="taskId">id of chosen task</param>
        /// <returns>returns text file</returns>
        [HttpGet("[action]/{taskId}")]
        public IActionResult ExportTaskAsXmlById(int taskId)
        {
            var task = taskService.GetTaskById(taskId);

            return new XmlResult(task);
        }

        /// <summary>
        /// Converts all user's tasks to csv format.
        /// </summary>
        /// <returns>return text file</returns>
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
        /// <returns>returns text file</returns>
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
        /// <returns>returns text file</returns>
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
        /// <param name="taskId">id of chosen task</param>
        /// <returns>returns text file</returns>
        [HttpGet("[action]/{taskId}")]
        public IActionResult ExportTaskAsCsvById(int taskId)
        {
            var task = taskService.GetTaskById(taskId);

            return new CsvResult(task);
        }

        /// <summary>
        /// Imports file of user's tasks in .csv format.
        /// </summary>
        /// <param name="uploadFile">file which user choses to import</param>
        /// <returns>result of importing file to database</returns>
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
        /// <param name="uploadFile">file which user choses to import</param>
        /// <returns>result of importing file to database</returns>
        [HttpPost("[action]")]
        public IActionResult ImportTasksAsXml(IFormFile uploadFile)
        {
            var userId = userIdentityService.GetUserId();
            var listOfTasks = taskService.ImportTasksFromXml(uploadFile, userId);

            return Ok(listOfTasks);
        }

        /// <summary>
        /// Returns all user's timers.
        /// </summary>
        /// <returns>result of getting all user's timers.</returns>
        [HttpGet("[action]")]
        public IActionResult GetAllTimersByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var tasks = taskService.GetAllTimersByUserId(userId);

            return Ok(tasks);
        }

        /// <summary>
        /// Returns all user's stopwatches.
        /// </summary>
        /// <returns>result of getting all user's stopwatches.</returns>
        [HttpGet("[action]")]
        public IActionResult GetAllStopwathesByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var tasks = taskService.GetAllStopwatchesByUserId(userId);

            return Ok(tasks);
        }
    }
}