using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Common.ModelsDTO;
using gtdtimer.Attributes;
using ServiceTier.Services;

namespace gtdtimer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService taskService;
        private readonly IUserIdentityService userIdentityService;

        public TasksController(
            ITaskService taskService,
            IUserIdentityService userIdentityService
            )
        {
            this.taskService = taskService;
            this.userIdentityService = userIdentityService;
        }


        [HttpGet("[action]")]
        public IActionResult GetAllTasks()
        {

            var allTasks = taskService.GetAllTasks();

            return Ok(allTasks);
        }

        [HttpGet("[action]")]
        public IActionResult GetAllActiveTasks()
        {
            var allTasks = taskService.GetAllActiveTasks();

            return Ok(allTasks);
        }

        [HttpGet("[action]")]
        public IActionResult GetAllArchivedTasks()
        {
            var allTasks = taskService.GetAllArchivedTasks();

            return Ok(allTasks);
        }

        [HttpGet("[action]")]
        public IActionResult GetTaskById(int taskId)
        {

            TaskDTO task = taskService.GetTaskById(taskId);

            return Ok(task);
        }

        [HttpGet("[action]")]
        public IActionResult GetAllTasksByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var tasks = taskService.GetAllTasksByUserId(userId);

            return Ok(tasks);
        }

        [HttpGet("[action]")]
        public IActionResult GetAllActiveTasksByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var tasks = taskService.GetAllActiveTasksByUserId(userId);

            return Ok(tasks);
        }

        [HttpGet("[action]")]
        public IActionResult GetAllArchivedTasksByUserId()
        {
            var userId = userIdentityService.GetUserId();
            var tasks = taskService.GetAllArchivedTasksByUserId(userId);

            return Ok(tasks);
        }

        [ValidateModel]
        [HttpPost("[action]")]
        public IActionResult CreateTask([FromBody]TaskDTO model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.CreateTask(model);

            return Ok();
        }

        [ValidateModel]
        [HttpPut("[action]")]
        public IActionResult UpdateTask([FromBody]TaskDTO model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.UpdateTask(model);

            return Ok();
        }

        [HttpDelete("DeleteTask/{taskId}")]
        public IActionResult DeleteTask(int taskId)
        {
            taskService.DeleteTaskById(taskId);

            return Ok();
        }

        [HttpPut("[action]")]
        public IActionResult UpdateTaskStatus(int taskId, bool newStatus)
        {
            taskService.UpdateTaskStatus(taskId, newStatus);

            return Ok();
        }

        [ValidateModel]
        [HttpPut("[action]")]
        public IActionResult StartTask([FromBody]TaskDTO model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.StartTask(model);

            return Ok("Timer has started.");
        }

        [ValidateModel]
        [HttpPut("[action]")]
        public IActionResult PauseTask([FromBody]TaskDTO model)
        {
            model.UserId = userIdentityService.GetUserId();
            taskService.PauseTask(model);

            return Ok("Timer has been paused.");
        }

        [HttpPut("[action]")]
        public IActionResult ResetTask(int taskId)
        {
            taskService.ResetTask(taskId);

            return Ok();
        }
    }
}