using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

using Common.ModelsDTO;

namespace ServiceTier.Services
{
    public interface ITaskService : IBaseService
    {
        IEnumerable<TaskDTO> GetAllTasks();
        IEnumerable<TaskDTO> GetAllTasksByUserId(int userId);
        IEnumerable<TaskDTO> GetAllActiveTasks();
        IEnumerable<TaskDTO> GetAllActiveTasksByUserId(int userId);
        IEnumerable<TaskDTO> GetAllArchivedTasks();
        IEnumerable<TaskDTO> GetAllArchivedTasksByUserId(int userId);
        TaskDTO GetTaskById(int taskId);
        void CreateTask(TaskDTO taskDTO);
        void UpdateTask(TaskDTO taskDTO);
        void DeleteTaskById(int taskId);
        void SwitchArchivedStatus(TaskDTO taskDTO);
        void ResetTask(TaskDTO taskDTO);
        void StartTask(TaskDTO taskDTO);
        void PauseTask(TaskDTO taskDTO);

        /// <summary>
        /// Method for importing user's tasks from .csv file.
        /// </summary>
        /// <param name="uploadFile">File being imported.</param>
        /// <param name="userId">Id of current user.</param>
        /// <returns></returns>
        IEnumerable<TaskDTO> ImportTasksFromCsv(IFormFile uploadFile, int userId);

        /// <summary>
        /// Method for importing user's tasks from .xml file.
        /// </summary>
        /// <param name="uploadFile">File being imported.</param>
        /// <param name="userId">Id of current user.</param>
        /// <returns></returns>
        IEnumerable<TaskDTO> ImportTasksFromXml(IFormFile uploadFile, int userId);

        /// <summary>
        /// Method for adding newly imported tasks to database.
        /// </summary>
        /// <param name="listOfTasksDto">List of tasks to be added.</param>
        /// <param name="userId">Id of current user.</param>
        /// <returns></returns>
        IEnumerable<TaskDTO> AddTaskToDatabase(IEnumerable<TaskDTO> listOfTasksDto, int userId);
    }
}