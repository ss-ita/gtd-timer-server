//-----------------------------------------------------------------------
// <copyright file="TaskServiceTests.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
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
        private readonly string contentCsv = "testData";
        private readonly string contentXml = @"<?xml version = ""1.0"" encoding=""utf-8""?>
                              <ArrayOfTaskDto xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                                <TaskDto>
                                    <Name>Test</Name>
                                    <Description />
                                    <ElapsedTime>0</ElapsedTime>
                                    <LastStartTime>0001-01-01T00:00:00</LastStartTime>
                                    <Goal xsi:nil=""true"" />
                                    <IsActive>true</IsActive>
                                    <IsRunning>true</IsRunning>
                                </TaskDto>
                              </ArrayOfTaskDto>";

        private readonly string fileNameCsv = "test.csv";
        private readonly string fileNameXml = "test.xml";
        private readonly DateTime start = DateTime.Now;
        private readonly DateTime end = DateTime.Now;
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
        /// Get All Tasks By Date test
        /// </summary>
        [Test]
        public void GetAllTasksByDate()
        {
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            unitOfWork.Setup(_ => _.Tasks.GetAllEntitiesByFilter(It.IsAny<Func<Tasks, bool>>())).Returns(tasks);

            Assert.AreEqual(subject.GetAllTasksByDate(userId, start, end).ToList()[0].Name, task.Name);
        }

        /// <summary>
        /// Get All Timers By Preset Id test
        /// </summary>
        [Test]
        public void GetAllTasksByPresetId()
        {
            int presetid = 12;
            Tasks task = new Tasks { Name = "task", Id = 1 };
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

            Assert.AreEqual(subject.GetAllTasksByPresetId(presetid)[0].Name, taskDtos[0].Name);
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

        /// <summary>
        /// AddTasksToDatabase method's unit test.
        /// </summary>
        [Test]
        public void AddTasksToDatabase()
        {
            List<TaskDto> listOfTasksDto = new List<TaskDto>();
            var taskRepository = new Mock<IRepository<Tasks>>();

            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            var result = subject.AddTaskToDatabase(listOfTasksDto, userId);

            Assert.IsInstanceOf(typeof(IEnumerable<TaskDto>), result);
            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// ImportTasksFromCsv method's unit test.
        /// </summary>
        [Test]
        public void ImportTasksFromCsv()
        {
            var fileMock = new Mock<IFormFile>();
            List<TaskDto> listOfTasksDto = new List<TaskDto>();

            var taskRepository = new Mock<IRepository<Tasks>>();
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            writer.Write(contentCsv);
            writer.Flush();
            memoryStream.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(memoryStream);
            fileMock.Setup(_ => _.FileName).Returns(fileNameCsv);
            fileMock.Setup(_ => _.Length).Returns(memoryStream.Length);
            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            var result = subject.ImportTasksFromCsv(fileMock.Object, userId);

            Assert.IsInstanceOf(typeof(IEnumerable<TaskDto>), result);
            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }

        /// <summary>
        /// ImportTasksFromXml method's unit test.
        /// </summary>
        [Test]
        public void ImportTasksFromXml()
        {
            var fileMock = new Mock<IFormFile>();
            List<TaskDto> listOfTasksDto = new List<TaskDto>();

            var taskRepository = new Mock<IRepository<Tasks>>();
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            writer.Write(contentXml);
            writer.Flush();
            memoryStream.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(memoryStream);
            fileMock.Setup(_ => _.FileName).Returns(fileNameXml);
            fileMock.Setup(_ => _.Length).Returns(memoryStream.Length);
            unitOfWork.Setup(_ => _.Tasks).Returns(taskRepository.Object);
            var result = subject.ImportTasksFromXml(fileMock.Object, userId);

            Assert.IsInstanceOf(typeof(IEnumerable<TaskDto>), result);
            unitOfWork.Verify(_ => _.Save(), Times.Once);
        }
    }
}