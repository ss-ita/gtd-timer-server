using Common.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;

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
        void UpdateTaskStatus(int taskId, bool newStatus);
        void ResetTask(int taskId);
        void StartTask(TaskDTO taskDTO);
        void PauseTask(TaskDTO taskDTO);
    }
}
