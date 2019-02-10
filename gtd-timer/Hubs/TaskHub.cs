using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

using GtdCommon.ModelsDto;
using GtdServiceTier.Services;

namespace GtdTimer.Hubs
{
    /// <summary>
    /// class for task hub
    /// </summary>
    [Authorize]
    public class TaskHub: Hub<ITaskClient>
    {
        /// <summary>
        /// Instance of task service
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// Instance of user identity service
        /// </summary>
        private readonly IUserIdentityService userIdentityService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskHub" /> class.
        /// </summary>
        /// <param name="taskService">instance of task service</param>
        public TaskHub(ITaskService taskService, IUserIdentityService userIdentityService)
        {
            this.taskService = taskService;
            this.userIdentityService = userIdentityService;
        }

        public async Task CreateTask(TaskDto model)
        {
            model.UserId = GetUserId();
            taskService.CreateTask(model);
            await Clients.Others.CreateTask(model);
        }

        public async Task StartTask(TaskDto model)
        {
            model.UserId = GetUserId();
            taskService.StartTask(model);
            await Clients.Others.StartTask(model);
        }

        public async Task PauseTask(TaskDto model)
        {
            model.UserId = GetUserId();
            taskService.PauseTask(model);
            await Clients.Others.PauseTask(model);
        }

        public async Task ResetTask(TaskDto model)
        {
            model.UserId = GetUserId();
            taskService.ResetTask(model);
            await Clients.Others.ResetTask(model);
        }

        public async Task DeleteTask(int taskId)
        {
            taskService.DeleteTaskById(taskId);
            await Clients.Others.DeleteTask(taskId);
        }

        public async Task UpdateTask(TaskDto model)
        {
            model.UserId = GetUserId();
            taskService.UpdateTask(model);
            await Clients.Others.UpdateTask(model);
        }

        private int GetUserId()
        {
            var userId = userIdentityService.GetUserId(Context.User.Identity);

            return userId;
        }
    }
}