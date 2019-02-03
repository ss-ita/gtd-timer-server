//-----------------------------------------------------------------------
// <copyright file="TaskDtoExtension.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;

using GtdCommon.ModelsDto;
using GtdTimerDAL.Entities;

namespace GtdTimerDAL.Extensions
{
    /// <summary>
    /// TaskDtoExtension class for converting to task and vice versa
    /// </summary>
    public static class TaskDtoExtension
    {
        /// <summary>
        /// Convert to task method
        /// </summary>
        /// <param name="taskDto"> taskDto model </param>
        /// <returns>returns task</returns>
        public static Tasks ToTask(this TaskDto taskDto)
        {
            return new Tasks
            {
                Id = taskDto.Id,
                Name = taskDto.Name,
                Description = taskDto.Description,
                ElapsedTime = TimeSpan.FromMilliseconds(taskDto.ElapsedTime),
                Goal = taskDto.Goal,
                LastStartTime = taskDto.LastStartTime,
                IsRunning = taskDto.IsRunning,
                UserId = taskDto.UserId,
                WatchType = taskDto.WatchType
            };
        }

        /// <summary>
        /// Convert to taskDto method
        /// </summary>
        /// <param name="task"> task model </param>
        /// <returns>returns taskDto</returns>
        public static TaskDto ToTaskDto(this Tasks task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                ElapsedTime = (int)task.ElapsedTime.TotalMilliseconds,
                Goal = task.Goal,
                LastStartTime = task.LastStartTime,
                IsRunning = task.IsRunning,
                UserId = task.UserId,
                WatchType = task.WatchType
            };
        }
    }
}
