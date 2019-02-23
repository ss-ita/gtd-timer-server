//-----------------------------------------------------------------------
// <copyright file="ITaskClient.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Threading.Tasks;

using GtdCommon.ModelsDto;

namespace GtdTimer.Hubs
{
    /// <summary>
    /// interface for task hub
    /// </summary>
    public interface ITaskClient
    {
        /// <summary>
        /// This event occurs when create a task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>Result of creating new task</returns>
        Task CreateTask(TaskDto model);

        /// <summary>
        /// This event occurs when start a task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>Result of starting task</returns>
        Task StartTask(TaskDto model);

        /// <summary>
        /// This event occurs when pause a task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>Result of pausing task</returns>
        Task PauseTask(TaskDto model);

        /// <summary>
        /// This event occurs when reset a task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>Result of reseting task</returns>
        Task ResetTask(TaskDto model);

        /// <summary>
        /// This event occurs when delete a task.
        /// </summary>
        /// <param name="taskId">task id</param>
        /// <returns>Result of deleting task</returns>
        Task DeleteTask(int taskId);

        /// <summary>
        /// This event occurs when update a task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>Result of updating task</returns>
        Task UpdateTask(TaskDto model);
    }
}
