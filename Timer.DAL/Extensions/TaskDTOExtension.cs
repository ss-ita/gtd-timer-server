using Timer.DAL.Timer.DAL.Entities;

using Common.ModelsDTO;
using System;

namespace Timer.DAL.Extensions
{
    public static class TaskDTOExtension
    {
        public static Tasks ToTask(this TaskDTO taskDTO)
        {
            Tasks task = new Tasks
            {
                Id = taskDTO.Id,
                Name = taskDTO.Name,
                Description = taskDTO.Description,
                ElapsedTime = TimeSpan.FromMilliseconds(taskDTO.ElapsedTime),
                Goal = taskDTO.Goal,
                LastStartTime = taskDTO.LastStartTime,
                IsActive = taskDTO.IsActive,
                IsRunning = taskDTO.IsRunning,
                UserId = taskDTO.UserId
            };

            return task;
        }

        public static TaskDTO ToTaskDTO(this Tasks task)
        {
            TaskDTO taskDTO = new TaskDTO
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                ElapsedTime = (int)task.ElapsedTime.TotalMilliseconds,
                Goal = task.Goal,
                LastStartTime = task.LastStartTime,
                IsActive = task.IsActive,
                IsRunning = task.IsRunning,
                UserId = task.UserId
            };

            return taskDTO;
        }
    }
}
