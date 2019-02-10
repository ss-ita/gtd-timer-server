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
        /// <returns></returns>
        Task CreateTask(TaskDto model);

        /// <summary>
        /// This event occurs when start a task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns></returns>
        Task StartTask(TaskDto model);

        /// <summary>
        /// This event occurs when pause a task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns></returns>
        Task PauseTask(TaskDto model);

        /// <summary>
        /// This event occurs when reset a task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns></returns>
        Task ResetTask(TaskDto model);

        /// <summary>
        /// This event occurs when delete a task.
        /// </summary>
        /// <param name="taskId">task id</param>
        /// <returns></returns>
        Task DeleteTask(int taskId);

        /// <summary>
        /// This event occurs when update a task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns></returns>
        Task UpdateTask(TaskDto model);
    }
}
