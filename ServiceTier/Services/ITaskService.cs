//-----------------------------------------------------------------------
// <copyright file="ITaskService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

using GtdCommon.ModelsDto;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// interface for task service class
    /// </summary>
    public interface ITaskService : IBaseService
    {
        /// <summary>
        /// Method for getting all tasks
        /// </summary>
        /// <returns> all tasks</returns>
        IEnumerable<TaskDto> GetAllTasks();

        /// <summary>
        /// Method for getting all tasks of chosen user
        /// </summary>
        /// <param name="userId">id of chosen user</param>
        /// <returns>all tasks of chosen user</returns>
        IEnumerable<TaskDto> GetAllTasksByUserId(int userId);

        /// <summary>
        /// Method for getting task by task id
        /// </summary>
        /// <param name="taskId">id of chosen task</param>
        /// <returns>task with chosen id</returns>
        TaskDto GetTaskById(int taskId);

        /// <summary>
        /// Method for creating a task
        /// </summary>
        /// <param name="taskDto">task model</param>
        void CreateTask(TaskDto taskDto);

        /// <summary>
        /// Method for updating a task
        /// </summary>
        /// <param name="taskDto">task model</param>
        void UpdateTask(TaskDto taskDto);

        /// <summary>
        /// Method for deleting a task by id
        /// </summary>
        /// <param name="taskId">id of chosen task</param>
        void DeleteTaskById(int taskId);

        /// <summary>
        /// Method for resetting a task
        /// </summary>
        /// <param name="taskDto">task model</param>
        void ResetTask(TaskDto taskDto);

        /// <summary>
        /// Method for starting a task
        /// </summary>
        /// <param name="taskDto">task model</param>
        void StartTask(TaskDto taskDto);

        /// <summary>
        /// Method for pausing a task
        /// </summary>
        /// <param name="taskDto">task model</param>
        void PauseTask(TaskDto taskDto);

        /// <summary>
        /// Method for importing user's tasks from .csv file.
        /// </summary>
        /// <param name="uploadFile">File being imported.</param>
        /// <param name="userId">Id of current user.</param>
        /// <returns>returns result of importing a file to datebase</returns>
        IEnumerable<TaskDto> ImportTasksFromCsv(IFormFile uploadFile, int userId);

        /// <summary>
        /// Method for importing user's tasks from .xml file.
        /// </summary>
        /// <param name="uploadFile">File being imported.</param>
        /// <param name="userId">Id of current user.</param>
        /// <returns>returns result of importing a file to datebase</returns>
        IEnumerable<TaskDto> ImportTasksFromXml(IFormFile uploadFile, int userId);

        /// <summary>
        /// Method for adding newly imported tasks to database.
        /// </summary>
        /// <param name="listOfTasksDto">List of tasks to be added.</param>
        /// <param name="userId">Id of current user.</param>
        /// <returns>returns result of importing a file to datebase</returns>
        IEnumerable<TaskDto> AddTaskToDatabase(IEnumerable<TaskDto> listOfTasksDto, int userId);

        /// <summary>
        /// Method for getting all users' Records.
        /// </summary>
        /// <param name="userId"> Id of user which records to return</param>
        /// <returns>result of getting all users' records.</returns>
        IEnumerable<TaskRecordDto> GetAllRecordsByUserId(int userId);

        /// <summary>
        /// Method for creating record
        /// </summary>
        /// <param name="taskRecord">TaskRecord model to create</param>
        /// <param name="userId">id of user for choosen record</param>
        void CreateRecord(TaskRecordDto taskRecord, int userId);

        /// <summary>
        /// Method for getting all records by task id
        /// </summary>
        /// <param name="userId">id of chosen user</param>
        /// <param name="taskId">id of chosen task</param>
        /// <returns>all records with id of chosen task</returns>
        IEnumerable<TaskRecordDto> GetAllRecordsByTaskId(int userId, int taskId);

        /// <summary>
        /// Method of deleting record by id
        /// </summary>
        /// <param name="taskId">Id of record to delete</param>
        void DeleteRecordById(int taskId);

        /// <summary>
        /// Method to reset and start task from history
        /// </summary>
        /// <param name="taskId">Id of task to reset</param>
        /// <returns>Returns taskRecordDto if task was stopped</returns>
        List<TaskRecordDto> ResetTaskFromHistory(int taskId);

        /// <summary>
        /// Method for getting all timers by preset id
        /// </summary>
        /// <param name="presetid">id of chosen preset</param>
        /// <returns>list of timers of chosen preset</returns>
        List<TaskDto> GetAllTasksByPresetId(int presetid);

        /// <summary>
        /// Method for getting all timers of chosen user.
        /// </summary>
        /// <param name="userId">Id of current user</param>
        /// <param name="start">Index of first element on the current page.</param>
        /// <param name="length">Length of the current page.</param>
        /// <returns>User's timers on a specific page.</returns>
        IEnumerable<TaskDto> GetAllTimersByUserId(int userId, int start = 0, int length = int.MaxValue);

        /// <summary>
        ///  Method for getting all stopwatches of chosen user.
        /// </summary>
        /// <param name="userId">Id of current user.</param>
        /// <param name="start">Index of first element on the current page.</param>
        /// <param name="length">Length of the current page.</param>
        /// <returns>List of all stopwatches of choosen user</returns>
        IEnumerable<TaskDto> GetAllStopwatchesByUserId(int userId, int start = 0, int length = int.MaxValue);

        /// <summary>
        /// Method for getting the total number of user's stopwacthes.
        /// </summary>
        /// <param name="userId">Id of current user.</param>
        /// <returns>Count of user's stopwatches.</returns>
        int GetAllStopwatchesByUserIdCount(int userId);

        /// <summary>
        /// Method for getting the total number of user's timers.
        /// </summary>
        /// <param name="userId">Id of current user.</param>
        /// <returns>Count of user's timers.</returns>
        int GetAllTimersByUserIdCount(int userId);

        /// <summary>
        /// Method for getting all tasks in specified period of time.
        /// </summary>
        /// <param name="userId">Id of current user.</param>
        /// <param name="start">Filter's start date.</param>
        /// <param name="end">Filter's end date.</param>
        /// <returns>Tasks of current user, filtered by date range.</returns>
        IEnumerable<TaskDto> GetAllTasksByDate(int userId, DateTime start, DateTime end);
    }
}