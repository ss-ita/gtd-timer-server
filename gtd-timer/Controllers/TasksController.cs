//-----------------------------------------------------------------------
// <copyright file="TasksController.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;
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
        /// <param name="start">start date filter</param>
        /// <param name="end">end date filter</param>
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
        /// <returns>result of exporting a tasks text file</returns>
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
        /// <returns>result of exporting a stopwatches text file</returns>
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
        /// <returns>result of exporting a timers text file</returns>
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
        /// <returns>result of exporting a chosen task text file</returns>
        [HttpGet("[action]/{taskId}")]
        public IActionResult ExportTaskAsXmlById(int taskId)
        {
            var task = taskService.GetTaskById(taskId);

            return new XmlResult(task);
        }

        /// <summary>
        /// Converts all user's tasks to csv format.
        /// </summary>
        /// <returns>result of exporting tasks by user id into text file</returns>
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
        /// <returns>result of exporting stopwatches by user id into a text file</returns>
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
        /// <returns>result of exporting timers by user id into a text file</returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllTimersAsCsvByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfTimers = taskService.GetAllTimersByUserId(userId);

            return new CsvResult(listOfTimers);
        }

        /// <summary>
        /// Converts all user's records to csv format.
        /// </summary>
        /// <returns>result of exporting all records by user id</returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllRecordsAsCsvByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfRecords = taskService.GetAllRecordsByUserId(userId);

            return new CsvResult(listOfRecords);
        }

        /// <summary>
        /// Converts all user's records to xml format.
        /// </summary>
        /// <returns>result of exporting all records by user id</returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllRecordsAsXmlByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfRecords = taskService.GetAllRecordsByUserId(userId);

            return new XmlResult(listOfRecords);
        }

        /// <summary>
        /// Converts all user's stopwatch records to csv format.
        /// </summary>
        /// <returns>result of exporting all stopwatch records by user id</returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllStopwatchesRecordsAsCsvByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfRecords = taskService.GetAllRecordsByUserId(userId);
            var listOfStopwatchesRecords = (from record in listOfRecords
                                           where record.WatchType == WatchType.Stopwatch
                                           select record).ToList();

            return new CsvResult(listOfStopwatchesRecords);
        }

        /// <summary>
        /// Converts all user's stopwatch records to xml format.
        /// </summary>
        /// <returns>result of exporting all stopwatch records by user id</returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllStopwatchesRecordsAsXmlByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfRecords = taskService.GetAllRecordsByUserId(userId);
            var listOfStopwatchesRecords = (from record in listOfRecords
                                           where record.WatchType == WatchType.Stopwatch
                                           select record).ToList();

            return new XmlResult(listOfStopwatchesRecords);
        }

        /// <summary>
        /// Converts all user's timer records to csv format.
        /// </summary>
        /// <returns>result of exporting all timer records by user id</returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllTimersRecordsAsCsvByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfRecords = taskService.GetAllRecordsByUserId(userId);
            var listOfTimersRecords = (from record in listOfRecords
                                      where record.WatchType == WatchType.Timer
                                      select record).ToList();

            return new CsvResult(listOfTimersRecords);
        }

        /// <summary>
        /// Converts all user's timer records to xml format.
        /// </summary>
        /// <returns>result of exporting all timer records by user id</returns>
        [HttpGet("[action]")]
        public IActionResult ExportAllTimersRecordsAsXmlByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var listOfRecords = taskService.GetAllRecordsByUserId(userId);
            var listOfTimersRecords = (from record in listOfRecords
                                      where record.WatchType == WatchType.Timer
                                      select record).ToList();

            return new XmlResult(listOfTimersRecords);
        }

        /// <summary>
        /// Converts  user's task by id to csv format.
        /// </summary>
        /// <param name="taskId">id of chosen task</param>
        /// <returns>result of exporting task by task id into a text file</returns>
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
            taskService.CreateRecord(taskRecord, userId);

            return Ok();
        }

        /// <summary>
        /// Returns all records by Task id
        /// </summary>
        /// <param name="taskId">Task id</param>
        /// <returns>Returns result of getting records by task id</returns>
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
        /// <param name="recordId">Id of record</param>
        /// <returns>Result of reseting task from history </returns>
        [HttpGet("[action]/{recordId}")]
        public IActionResult ResetTaskFromHistory(int recordId)
        {
            var recordToReturn = taskService.ResetTaskFromHistory(recordId);

            return Ok(recordToReturn);
        }

        /// <summary>
        /// Returns all user's timers by page.
        /// </summary>
        /// <param name="start">Index of first element on current page.</param>
        /// <param name="length">Length of current page.</param>
        /// <returns>Result of getting all user's timers by page.</returns>
        [HttpGet("[action]")]
        public IActionResult GetAllTimersByUserId(int start = 0, int length = int.MaxValue)
        {
            var userId = userIdentityService.GetUserId();
            var tasks = taskService.GetAllTimersByUserId(userId, start, length);

            return Ok(tasks);
        }

        /// <summary>
        /// Returns all user's stopwatches by page.
        /// </summary>
        /// <param name="start">Index of first element on current page.</param>
        /// <param name="length">Length of current page.</param>
        /// <returns>Result of getting all user's stopwatches by page.</returns>
        [HttpGet("[action]")]
        public IActionResult GetAllStopwatchesByUserId(int start = 0, int length = int.MaxValue)
        {
            var userId = userIdentityService.GetUserId();
            var tasks = taskService.GetAllStopwatchesByUserId(userId, start, length);

            return Ok(tasks);
        }

        /// <summary>
        /// Returns total number of user's stopwatches.
        /// </summary>
        /// <returns>Result of getting all user's stopwatches count.</returns>
        [HttpGet("[action]")]
        public IActionResult GetAllStopwatchesByUserIdCount()
        {
            var userId = userIdentityService.GetUserId();
            var stopwatchesCount = taskService.GetAllStopwatchesByUserIdCount(userId);

            return Ok(stopwatchesCount);
        }

        /// <summary>
        /// Returns total number of user's timers.
        /// </summary>
        /// <returns>Result of getting all user's timers count.</returns>
        [HttpGet("[action]")]
        public IActionResult GetAllTimersByUserIdCount()
        {
            var userId = userIdentityService.GetUserId();
            var timersCount = taskService.GetAllTimersByUserIdCount(userId);

            return Ok(timersCount);
        }
    }
}