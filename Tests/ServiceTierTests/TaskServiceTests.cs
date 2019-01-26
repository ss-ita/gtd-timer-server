//-----------------------------------------------------------------------
// <copyright file="TaskServiceTests.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;

using GtdCommon.Exceptions;
using GtdCommon.ModelsDto;
using GtdServiceTier.Services;
using GtdTimerDAL.Entities;
using GtdTimerDAL.Repositories;
using GtdTimerDAL.UnitOfWork;
using GtdTimerDAL.Extensions;

namespace GtdServiceTierTests
{
    public class TaskServiceTests
    {
        private readonly int userId = 1;
        private Tasks task = new Tasks { Id = 1, Name = "Test", UserId = 1 };
        private List<Tasks> tasks = new List<Tasks>();
        private Mock<IUnitOfWork> unitOfWork;
        private TaskService subject;

        /// <summary>
        /// Method which is called immediately in each test run
        /// </summary>
        [SetUp]
        public void Setup()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            subject = new TaskService(unitOfWork.Object);
            tasks.Add(task);
        }

        /// <summary>
        /// Create Task test
        /// </summary>
        [Test]
        public void CreateTask()
        {
            TaskDto task = new TaskDto();
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            subject.CreateTask(task);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Update Task test
        /// </summary>
        [Test]
        public void UpdateTask()
        {
            TaskDto task = new TaskDto();
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            subject.UpdateTask(task);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Delete Task test
        /// </summary>
        [Test]
        public void DeleteTask()
        {
            int taskId = 2;
            Tasks task = new Tasks();
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            unitOfWork.Setup(_ => _.Tasks.GetByID(taskId)).Returns(task);

            subject.DeleteTaskById(taskId);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Get Task By Id test
        /// </summary>
        [Test]
        public void GetTaskById()
        {
            int taskId = 2;
            Tasks task = new Tasks();
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            unitOfWork.Setup(_ => _.Tasks.GetByID(taskId)).Returns(task);

            Assert.AreEqual(subject.GetTaskById(taskId).Name, task.Name);
        }

        /// <summary>
        /// Switch Task Status test
        /// </summary>
        [Test]
        public void UpdateTaskStatus()
        {
            TaskDto task = new TaskDto();
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            subject.SwitchArchivedStatus(task);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Reset Task test
        /// </summary>
        [Test]
        public void ResetTask()
        {
            TaskDto task = new TaskDto();
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            subject.ResetTask(task);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Start Task test
        /// </summary>
        [Test]
        public void StartTask()
        {
            TaskDto task = new TaskDto();
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            subject.StartTask(task);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Pause Task test
        /// </summary>
        [Test]
        public void PauseTask()
        {
            TaskDto task = new TaskDto();
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            subject.PauseTask(task);

            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// Get Throws Task Not Found Exception test
        /// </summary>
        [Test]
        public void Get_ThrowsTaskNotFoundException()
        {
            int taskId = 500;
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);

            var ex = Assert.Throws<TaskNotFoundException>(() => subject.GetTaskById(taskId));

            Assert.That(ex.Message, Is.EqualTo("Task does not exist!"));
        }

        /// <summary>
        /// Get All Tasks test
        /// </summary>
        [Test]
        public void GetAllTasks()
        {
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            unitOfWork.Setup(_ => _.Tasks.GetAllEntities()).Returns(tasks);

            Assert.AreEqual(subject.GetAllTasks().ToList()[0].Name, task.Name);
        }

        /// <summary>
        /// Get All Tasks By User Id test
        /// </summary>
        [Test]
        public void GetAllTasksByUserId()
        {
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            unitOfWork.Setup(_ => _.Tasks.GetAllEntitiesByFilter(It.IsAny<Func<Tasks, bool>>())).Returns(tasks);

            Assert.AreEqual(subject.GetAllTasksByUserId(userId).ToList()[0].Name, task.Name);
        }

        /// <summary>
        /// Get All Active Tasks test
        /// </summary>
        [Test]
        public void GetAllActiveTasks()
        {
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            unitOfWork.Setup(_ => _.Tasks.GetAllEntitiesByFilter(It.IsAny<Func<Tasks, bool>>())).Returns(tasks);

            Assert.AreEqual(subject.GetAllActiveTasks().ToList()[0].Name, task.Name);
        }

        /// <summary>
        /// Get All Active Tasks By User Id test
        /// </summary>
        [Test]
        public void GetAllActiveTasksByUserId()
        {
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            unitOfWork.Setup(_ => _.Tasks.GetAllEntitiesByFilter(It.IsAny<Func<Tasks, bool>>())).Returns(tasks);

            Assert.AreEqual(subject.GetAllActiveTasksByUserId(userId).ToList()[0].Name, task.Name);
        }

        /// <summary>
        /// Get All Archived Tasks test
        /// </summary>
        [Test]
        public void GetAllArchivedTasks()
        {
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            unitOfWork.Setup(_ => _.Tasks.GetAllEntitiesByFilter(It.IsAny<Func<Tasks, bool>>())).Returns(tasks);

            Assert.AreEqual(subject.GetAllArchivedTasks().ToList()[0].Name, task.Name);
        }

        /// <summary>
        /// Get All Archived Tasks By User Id test
        /// </summary>
        [Test]
        public void GetAllArchivedTasksByUserId()
        {
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            unitOfWork.Setup(_ => _.Tasks.GetAllEntitiesByFilter(It.IsAny<Func<Tasks, bool>>())).Returns(tasks);

            Assert.AreEqual(subject.GetAllArchivedTasksByUserId(userId).ToList()[0].Name, task.Name);
        }

        /// <summary>
        /// Get All Timers By Preset Id test
        /// </summary>
        [Test]
        public void GetAllTasksByPresetId()
        {
            int presetid = 12;
            Tasks task = new Tasks { Name = "task", Id = 1, IsActive = true };
            List<Tasks> tasks = new List<Tasks>
            {
                task
            };
            PresetTasks presetTasks = new PresetTasks { Id = 1, PresetId = presetid, TaskId = task.Id };
            List<PresetTasks> presetsTasks = new List<PresetTasks>
            {
                presetTasks
            };
            List<TaskDto> taskDtos = new List<TaskDto>
            {
                task.ToTaskDto()
            };
            var tasksRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(tasksRepository.Object);
            unitOfWork.Setup(_ => _.Tasks.GetAllEntitiesByFilter(It.IsAny<Func<Tasks, bool>>())).Returns(tasks);
            unitOfWork.Setup(_ => _.PresetTasks.GetAllEntitiesByFilter(It.IsAny<Func<PresetTasks, bool>>())).Returns(presetsTasks);
            unitOfWork.Setup(_ => _.Tasks.GetByID(task.Id)).Returns(task);

            Assert.AreEqual(subject.GetAllTasksByPresetId(presetid)[0].IsActive, taskDtos[0].IsActive);
        }

        /// <summary>
        /// Get All Timers By User Id test
        /// </summary>
        [Test]
        public void GetAllTimersByUserId()
        {
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            unitOfWork.Setup(_ => _.Tasks.GetAllEntitiesByFilter(It.IsAny<Func<Tasks, bool>>())).Returns(tasks);

            Assert.AreEqual(subject.GetAllTimersByUserId(userId).ToList()[0].Name, task.Name);
        }

        /// <summary>
        /// Get All Stopwatches By User Id test
        /// </summary>
        [Test]
        public void GetAllStopwatchesByUserId()
        {
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            unitOfWork.Setup(_ => _.Tasks.GetAllEntitiesByFilter(It.IsAny<Func<Tasks, bool>>())).Returns(tasks);

            Assert.AreEqual(subject.GetAllStopwatchesByUserId(userId).ToList()[0].Name, task.Name);
        }
    }
}