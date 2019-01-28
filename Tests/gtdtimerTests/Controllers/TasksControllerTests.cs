//-----------------------------------------------------------------------
// <copyright file="TasksControllerTests.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

using GtdCommon.ModelsDto;
using GtdTimer.Controllers;
using GtdServiceTier.Services;
using GtdTimerDAL.UnitOfWork;
using GtdTimer.ActionResults;
using Microsoft.AspNetCore.Http;

namespace GtdTimerTests.Controllers
{
    [TestFixture]
    public class TasksControllerTests : ControllerBase
    {
        private Mock<ITaskService> taskService;
        private Mock<IUserIdentityService> userIdentityService;
        private Mock<IUnitOfWork> unitOfWork;
        private TasksController subject;
        private int userId = 315;
        private int taskId = 9;

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            taskService = new Mock<ITaskService>();
            userIdentityService = new Mock<IUserIdentityService>();
            unitOfWork = new Mock<IUnitOfWork>();
            subject = new TasksController(taskService.Object, userIdentityService.Object);
        }

        /// <summary>
        /// Get Task By Id test
        /// </summary>
        [Test]
        public void GetTaskById()
        {
            TaskDto taskDto = new TaskDto();
            taskService.Setup(task => task.GetTaskById(taskId)).Returns(taskDto);

            var actual = (OkObjectResult)subject.GetTaskById(taskId);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, taskDto);
        }

        /// <summary>
        /// Get All Tasks test
        /// </summary>
        [Test]
        public void GetAllTasks()
        {
            List<TaskDto> tasks = new List<TaskDto>();
            taskService.Setup(_ => _.GetAllTasks()).Returns(tasks);
            var actual = (OkObjectResult)subject.GetAllTasks();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, tasks);
        }

        /// <summary>
        /// Get All Active Tasks test
        /// </summary>
        [Test]
        public void GetAllActiveTasks()
        {
            List<TaskDto> tasks = new List<TaskDto>();
            taskService.Setup(_ => _.GetAllActiveTasks()).Returns(tasks);
            var actual = (OkObjectResult)subject.GetAllActiveTasks();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, tasks);
        }

        /// <summary>
        /// Get All Archived Tasks test
        /// </summary>
        [Test]
        public void GetAllArchivedTasks()
        {
            List<TaskDto> tasks = new List<TaskDto>();
            taskService.Setup(_ => _.GetAllArchivedTasks()).Returns(tasks);
            var actual = (OkObjectResult)subject.GetAllArchivedTasks();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, tasks);
        }

        /// <summary>
        /// Get All Tasks By User Id test
        /// </summary>
        [Test]
        public void GetAllTasksByUserId()
        {
            List<TaskDto> tasks = new List<TaskDto>();
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            taskService.Setup(_ => _.GetAllTasksByUserId(userId)).Returns(tasks);
            var actual = (OkObjectResult)subject.GetAllTasksByUserId();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, tasks);
        }

        /// <summary>
        /// Get All Active Tasks By User Id test
        /// </summary>
        [Test]
        public void GetAllActiveTasksByUserId()
        {
            List<TaskDto> tasks = new List<TaskDto>();
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            taskService.Setup(_ => _.GetAllActiveTasksByUserId(userId)).Returns(tasks);
            var actual = (OkObjectResult)subject.GetAllActiveTasksByUserId();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, tasks);
        }

        /// <summary>
        /// Get All Archived Tasks By User Id test
        /// </summary>
        [Test]
        public void GetAllArchivedTasksByUserId()
        {
            List<TaskDto> tasks = new List<TaskDto>();
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            taskService.Setup(_ => _.GetAllArchivedTasksByUserId(userId)).Returns(tasks);
            var actual = (OkObjectResult)subject.GetAllArchivedTasksByUserId();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, tasks);
        }

        /// <summary>
        /// Create Task test
        /// </summary>
        [Test]
        public void CreateTask()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            TaskDto model = new TaskDto();
            var actual = (OkObjectResult)subject.CreateTask(model);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            taskService.Verify(_ => _.CreateTask(model), Times.Once);
        }

        /// <summary>
        /// Update Task test
        /// </summary>
        [Test]
        public void UpdateTask()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            TaskDto model = new TaskDto();
            var actual = (OkResult)subject.UpdateTask(model);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            taskService.Verify(_ => _.UpdateTask(model), Times.Once);
        }

        /// <summary>
        /// Delete Task test
        /// </summary>
        [Test]
        public void DeleteTask()
        {
            var actual = (OkResult)subject.DeleteTask(taskId);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            taskService.Verify(_ => _.DeleteTaskById(taskId), Times.Once);
        }

        /// <summary>
        /// Switch Archived Status test
        /// </summary>
        [Test]
        public void SwitchArchivedStatus()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            TaskDto model = new TaskDto();
            var actual = (OkResult)subject.SwitchArchivedStatus(model);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            taskService.Verify(_ => _.SwitchArchivedStatus(model), Times.Once);
        }

        /// <summary>
        /// Reset Task test
        /// </summary>
        [Test]
        public void ResetTask()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            TaskDto model = new TaskDto();
            var actual = (OkResult)subject.ResetTask(model);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            taskService.Verify(_ => _.ResetTask(model), Times.Once);
        }

        /// <summary>
        /// Start Task test
        /// </summary>
        [Test]
        public void StartTask()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            TaskDto model = new TaskDto();
            var actual = (OkResult)subject.StartTask(model);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            taskService.Verify(_ => _.StartTask(model), Times.Once);
        }

        /// <summary>
        /// Pause Task test
        /// </summary>
        [Test]
        public void PauseTask()
        {
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            TaskDto model = new TaskDto();
            var actual = (OkResult)subject.PauseTask(model);

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            taskService.Verify(_ => _.PauseTask(model), Times.Once);
        }

        /// <summary>
        /// Get All Timers By User Id test
        /// </summary>
        [Test]
        public void GetAllTimersByUserId()
        {
            List<TaskDto> tasks = new List<TaskDto>();
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            taskService.Setup(_ => _.GetAllTimersByUserId(userId)).Returns(tasks);
            var actual = (OkObjectResult)subject.GetAllTimersByUserId();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, tasks);
        }

        /// <summary>
        /// Get All Timers By User Id test
        /// </summary>
        [Test]
        public void GetAllStopwatchesByUserId()
        {
            List<TaskDto> tasks = new List<TaskDto>();
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            taskService.Setup(_ => _.GetAllStopwatchesByUserId(userId)).Returns(tasks);
            var actual = (OkObjectResult)subject.GetAllStopwathesByUserId();

            Assert.AreEqual(actual.StatusCode, (int)HttpStatusCode.OK);
            Assert.AreSame(actual.Value, tasks);
        }

        /// <summary>
        /// ExportAllTasksAsXmlByUserId method's unit test.
        /// </summary>
        [Test]
        public void ExportAllTasksAsXmlByUserId()
        {
            List<TaskDto> tasks = new List<TaskDto>();
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            taskService.Setup(_ => _.GetAllTasksByUserId(userId)).Returns(tasks);
            var actual = subject.ExportAllTasksAsXmlByUserId();

            Assert.IsInstanceOf(typeof(XmlResult), actual);
            Assert.AreSame(tasks, (actual as XmlResult).ObjectToSerialize);
        }

        /// <summary>
        /// ExportAllTasksAsCsvByUserId method's unit test.
        /// </summary>
        [Test]
        public void ExportAllTasksAsCsvByUserId()
        {
            List<TaskDto> tasks = new List<TaskDto>();
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            taskService.Setup(_ => _.GetAllTasksByUserId(userId)).Returns(tasks);
            var actual = subject.ExportAllTasksAsCsvByUserId();

            Assert.IsInstanceOf(typeof(CsvResult), actual);
            Assert.AreSame(tasks, (actual as CsvResult).ObjectToSerialize);
        }

        /// <summary>
        /// ExportTaskAsXmlById method's unit test.
        /// </summary>
        [Test]
        public void ExportTaskAsXmlById()
        {
            TaskDto taskDto = new TaskDto();
            taskService.Setup(task => task.GetTaskById(taskId)).Returns(taskDto);

            var actual = subject.ExportTaskAsXmlById(taskId);

            Assert.IsInstanceOf(typeof(XmlResult), actual);
            Assert.AreSame(taskDto, (actual as XmlResult).ObjectToSerialize);
        }

        /// <summary>
        /// ImportTasksAsCsv method's unit test.
        /// </summary>
        [Test]
        public void ImportTasksAsCsv()
        {
            var fileMock = new Mock<IFormFile>();
            List<TaskDto> listOfTasksDto = new List<TaskDto>();

            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            taskService.Setup(task => task.ImportTasksFromCsv(fileMock.Object, userId)).Returns(listOfTasksDto);
            var actual = subject.ImportTasksAsCsv(fileMock.Object);

            Assert.IsInstanceOf(typeof(OkObjectResult), actual);
            Assert.AreSame(listOfTasksDto, (actual as OkObjectResult).Value);
        }

        /// <summary>
        /// ImportTasksAsXml method's unit test.
        /// </summary>
        [Test]
        public void ImportTasksAsXml()
        {
            var fileMock = new Mock<IFormFile>();
            List<TaskDto> listOfTasksDto = new List<TaskDto>();
            userIdentityService.Setup(_ => _.GetUserId()).Returns(userId);
            taskService.Setup(task => task.ImportTasksFromXml(fileMock.Object, userId)).Returns(listOfTasksDto);
            var actual = subject.ImportTasksAsXml(fileMock.Object);

            Assert.IsInstanceOf(typeof(OkObjectResult), actual);
            Assert.AreSame(listOfTasksDto, (actual as OkObjectResult).Value);
        }

        /// <summary>
        /// ExportTaskAsCsvById method's unit test.
        /// </summary>
        [Test]
        public void ExportTaskAsCsvById()
        {
            TaskDto taskDto = new TaskDto();
            taskService.Setup(task => task.GetTaskById(taskId)).Returns(taskDto);
            var actual = subject.ExportTaskAsCsvById(taskId);

            Assert.IsInstanceOf(typeof(CsvResult), actual);
            Assert.AreSame(taskDto, (actual as CsvResult).ObjectToSerialize);
        }
    }
}
