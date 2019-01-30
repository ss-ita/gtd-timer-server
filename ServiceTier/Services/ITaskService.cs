//-----------------------------------------------------------------------
// <copyright file="ITaskService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

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
        /// Method for getting all active tasks
        /// </summary>
        /// <returns>all active tasks</returns>
        IEnumerable<TaskDto> GetAllActiveTasks();

        /// <summary>
        /// Method for getting all active tasks of chosen user
        /// </summary>
        /// <param name="userId">id of chosen user</param>
        /// <returns>all active tasks of chosen user</returns>
        IEnumerable<TaskDto> GetAllActiveTasksByUserId(int userId);

        /// <summary>
        /// Method for getting all archived tasks
        /// </summary>
        /// <returns>returns list of all archived tasks</returns>
        IEnumerable<TaskDto> GetAllArchivedTasks();

        /// <summary>
        /// Method for getting all archived tasks of chosen user
        /// </summary>
        /// <param name="userId">id of chosen user</param>
        /// <returns>all archived tasks of chosen user</returns>
        IEnumerable<TaskDto> GetAllArchivedTasksByUserId(int userId);

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
        /// Method for switching status of task from archived to active and vice versa
        /// </summary>
        /// <param name="taskDto">task model</param>
        void SwitchArchivedStatus(TaskDto taskDto);

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
        void CreateRecord(TaskRecordDto taskRecord);

        /// <summary>
        /// Method for getting all records by task id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        IEnumerable<TaskRecordDto> GetAllRecordsByTaskId(int userId,int taskId);

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
        TaskRecordDto ResetTaskFromHistory(int taskId);

        /// <summary>
        /// Method for getting all timers by preset id
        /// </summary>
        /// <param name="presetid">id of chosen preset</param>
        /// <returns>list of timers of chosen preset</returns>
        List<TaskDto> GetAllTasksByPresetId(int presetid);

        /// <summary>
        /// Method for getting all timers of chosen user
        /// </summary>
        /// <param name="userId">id of chosen user</param>
        /// <returns>all timers of chosen user</returns>
        IEnumerable<TaskDto> GetAllTimersByUserId(int userId);

        /// <summary>
        /// Method for getting all stopwatches of chosen user
        /// </summary>
        /// <param name="userId">id of chosen user</param>
        /// <returns>all stopwatches of chosen user</returns>
        IEnumerable<TaskDto> GetAllStopwatchesByUserId(int userId);
    }
}