//-----------------------------------------------------------------------
// <copyright file="TaskHub.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using GtdCommon.ModelsDto;
using GtdServiceTier.Services;

namespace GtdTimer.Hubs
{
    /// <summary>
    /// class for task hub
    /// </summary>
    [Authorize]
    public class TaskHub : Hub<ITaskClient>
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
        /// <param name="userIdentityService">instance of user identity service</param>
        public TaskHub(ITaskService taskService, IUserIdentityService userIdentityService)
        {
            this.taskService = taskService;
            this.userIdentityService = userIdentityService;
        }

        /// <summary>
        /// This event occurs when create a task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>Result of creating new task</returns>
        public async Task CreateTask(TaskDto model)
        {
            model.UserId = GetUserId();
            taskService.CreateTask(model);
            await Clients.User(Context.UserIdentifier).CreateTask(model);
        }

        /// <summary>
        /// This event occurs when start a task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>Result of starting task</returns>
        public async Task StartTask(TaskDto model)
        {
            model.UserId = GetUserId();
            taskService.StartTask(model);
            await Clients.GroupExcept(Context.UserIdentifier, Context.ConnectionId).StartTask(model);
        }

        /// <summary>
        /// This event occurs when pause a task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>Result of pausing task</returns>
        public async Task PauseTask(TaskDto model)
        {
            model.UserId = GetUserId();
            taskService.PauseTask(model);
            await Clients.GroupExcept(Context.UserIdentifier, Context.ConnectionId).PauseTask(model);
        }

        /// <summary>
        /// This event occurs when reset a task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>Result of reseting task</returns>
        public async Task ResetTask(TaskDto model)
        {
            model.UserId = GetUserId();
            taskService.ResetTask(model);
            await Clients.GroupExcept(Context.UserIdentifier, Context.ConnectionId).ResetTask(model);
        }

        /// <summary>
        /// This event occurs when delete a task.
        /// </summary>
        /// <param name="taskId">task id</param>
        /// <returns>Result of deleting task</returns>
        public async Task DeleteTask(int taskId)
        {
            taskService.DeleteTaskById(taskId);
            await Clients.GroupExcept(Context.UserIdentifier, Context.ConnectionId).DeleteTask(taskId);
        }

        /// <summary>
        /// This event occurs when update a task.
        /// </summary>
        /// <param name="model">task model</param>
        /// <returns>Result of updating task</returns>
        public async Task UpdateTask(TaskDto model)
        {
            model.UserId = GetUserId();
            taskService.UpdateTask(model);
            await Clients.GroupExcept(Context.UserIdentifier, Context.ConnectionId).UpdateTask(model);
        }

        /// <summary>
        /// Method for connecting to hub
        /// </summary>
        /// <returns>result of connecting to hub</returns>
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.UserIdentifier);
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Method for disconnecting from hub
        /// </summary>
        /// <param name="exception">exeption result of disconecting from hub</param>
        /// <returns>result of disconnecting from hub</returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.UserIdentifier);
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Method for getting user id
        /// </summary>
        /// <returns>user id</returns>
        private int GetUserId()
        {
            var userId = userIdentityService.GetUserId(Context.User.Identity);

            return userId;
        }
    }
}