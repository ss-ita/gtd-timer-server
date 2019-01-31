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
using System;

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
        /// Returns all user's tasks within specified date range.
        /// </summary>
        /// <returns>result of getting all user's tasks within date range.</returns>
        [HttpGet("[action]")]
        public IActionResult GetAllTasksByDate(DateTime start, DateTime end)
        {
            var userId = userIdentityService.GetUserId();
            var tasks = taskService.GetAllTasksByDate(userId, start, end);

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
        /// Converts all user's stopwatches to xml format. 
        /// </summary>
        /// <returns>returns a text file</returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllStopwatchesAsXmlByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfStopwatches = taskService.GetAllStopwatchesByUserId(userId);

            return new XmlResult(listOfStopwatches);
        }

        /// <summary>
        /// Converts all user's timers to xml format. 
        /// </summary>
        /// <returns>returns a text file</returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllTimersAsXmlByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfTimers = taskService.GetAllTimersByUserId(userId);

            return new XmlResult(listOfTimers);
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
        /// Converts all user's stopwatches to csv format.
        /// </summary>
        /// <returns>return text file</returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllStopwatchesAsCsvByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfStopwatches = taskService.GetAllStopwatchesByUserId(userId);

            return new CsvResult(listOfStopwatches);
        }

        /// <summary>
        /// Converts all user's stopwatches to csv format.
        /// </summary>
        /// <returns>return text file</returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllTimersAsCsvByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfTimers = taskService.GetAllTimersByUserId(userId);

            return new CsvResult(listOfTimers);
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
        /// Returns all users' Records.
        /// </summary>
        /// <returns>result of getting all users' records.</returns>
        [HttpGet("[action]")]
        public IActionResult GetAllRecordsByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var taskRecords = taskService.GetAllRecordsByUserId(userId);

            return Ok(taskRecords);
        }

        /// <summary>
        /// Create record
        /// </summary>
        /// <param name="taskRecord">TaskRecordDto model </param>
        /// <returns>Returns result of creating record</returns>
        [HttpPost("[action]")]
        public IActionResult CreateRecord([FromBody]TaskRecordDto taskRecord)
        {
            var userId = userIdentityService.GetUserId();
            taskService.CreateRecord(taskRecord);

            return Ok();
        }

        /// <summary>
        /// Returns all records by Task id
        /// </summary>
        /// <param name="taskId">Task id</param>
        /// <returns>Return result of getting records by task id</returns>
        [HttpGet("[action]/{taskId}")]
        public IActionResult GetAllRecordsByTaskId(int taskId)
        {
            var userId = userIdentityService.GetUserId();
            var taskRecords = taskService.GetAllRecordsByTaskId(userId, taskId);

            return Ok(taskRecords);
        }

        /// <summary>
        /// Delete Record by id
        /// </summary>
        /// <param name="taskId">Id of record to delete</param>
        /// <returns>Results of deleting record</returns>
        [HttpDelete("[action]/{taskId}")]
        public IActionResult DeleteRecordById(int taskId)
        {
            taskService.DeleteRecordById(taskId);

            return Ok();
        }

        /// <summary>
        /// Reset and run task 
        /// </summary>
        /// <param name="recordId">Id of task to reset</param>
        /// <returns>If task is runned it returns the new record</returns>
        [HttpGet("[action]/{taskId}")]
        public IActionResult ResetTaskFromHistory(int taskId)
        {
            var recordToReturn = taskService.ResetTaskFromHistory(taskId);

            return Ok(recordToReturn);
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