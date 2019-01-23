﻿//-----------------------------------------------------------------------
// <copyright file="TaskService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

using DefaultXmlSerializer = System.Xml.Serialization.XmlSerializer;
using GtdCommon.Exceptions;
using GtdCommon.ModelsDto;
using GtdTimerDAL.Extensions;
using GtdTimerDAL.Entities;
using GtdTimerDAL.UnitOfWork;
using ServiceStack.Text;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// class which implements i unit of work interface
    /// </summary>
    public class TaskService : BaseService, ITaskService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskService" /> class.
        /// </summary>
        /// <param name="unitOfWork">instance of unit of work</param>
        public TaskService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void CreateTask(TaskDto taskDto)
        {
            var task = taskDto.ToTask();
            UnitOfWork.Tasks.Create(task);
            UnitOfWork.Save();
        }

        public IEnumerable<TaskDto> AddTaskToDatabase(IEnumerable<TaskDto> listOfTasksDto, int userId)
        {
            var newTasks = new List<Tasks>();

            foreach(var taskDto in listOfTasksDto)
            {
                taskDto.UserId = userId;
                var task = taskDto.ToTask();
                UnitOfWork.Tasks.Create(task);
                newTasks.Add(task);
            }

            UnitOfWork.Save();

            return newTasks.Select(task => task.ToTaskDto());
        }

        public void DeleteTaskById(int taskId)
        {
            var toDelete = UnitOfWork.Tasks.GetByID(taskId);
            if (toDelete != null)
            {
                UnitOfWork.Tasks.Delete(toDelete);
                UnitOfWork.Save();
            }
            else
            {
                throw new TaskNotFoundException();
            }
        }

        public IEnumerable<TaskDto> GetAllTasks()
        {
            var listOfTasksDto = UnitOfWork.Tasks.GetAllEntities()
                .Select(task => task.ToTaskDto())
                .ToList();

            return listOfTasksDto;
        }

        public IEnumerable<TaskDto> GetAllTasksByUserId(int userId)
        {
            var listOfTasksDto = UnitOfWork.Tasks.GetAllEntitiesByFilter(user => user.UserId == userId)
                .Select(task => task.ToTaskDto())
                .ToList();

            return listOfTasksDto;
        }

        public TaskDto GetTaskById(int taskId)
        {
            Tasks task = UnitOfWork.Tasks.GetByID(taskId);
            if (task == null)
            {
                throw new TaskNotFoundException();
            }

            return task.ToTaskDto();
        }

        public void UpdateTask(TaskDto taskDto)
        {
            var task = taskDto.ToTask();

            UnitOfWork.Tasks.Update(task);
            UnitOfWork.Save();
        }

        public void SwitchArchivedStatus(TaskDto model)
        {
            model.IsActive = !model.IsActive;

            var task = model.ToTask();

            UnitOfWork.Tasks.Update(task);
            UnitOfWork.Save();
        }

        public void ResetTask(TaskDto model)
        {
            model.ElapsedTime = 0;
            model.Goal = null;
            model.LastStartTime = model.LastStartTime.Date;
            model.IsRunning = false;

            var task = model.ToTask();

            UnitOfWork.Tasks.Update(task);
            UnitOfWork.Save();
        }

        public void StartTask(TaskDto model)
        {
            model.IsRunning = true;
            var task = model.ToTask();

            UnitOfWork.Tasks.Update(task);
            UnitOfWork.Save();
        }

        public void PauseTask(TaskDto model)
        {
            model.IsRunning = false;
            var task = model.ToTask();

            UnitOfWork.Tasks.Update(task);
            UnitOfWork.Save();
        }

        public IEnumerable<TaskDto> GetAllActiveTasks()
        {
            var listOfTasksDto = UnitOfWork.Tasks.GetAllEntitiesByFilter(task => task.IsActive == true)
                .Select(task => task.ToTaskDto())
                .ToList();

            return listOfTasksDto;
        }

        public IEnumerable<TaskDto> GetAllActiveTasksByUserId(int userId)
        {
            var listOfTasksDto = UnitOfWork.Tasks.GetAllEntitiesByFilter((task) => (task.UserId == userId && task.IsActive == true))
                .Select(task => task.ToTaskDto())
                .ToList();

            return listOfTasksDto;
        }

        public IEnumerable<TaskDto> GetAllArchivedTasks()
        {
            var listOfTasksDto = UnitOfWork.Tasks.GetAllEntitiesByFilter(task => task.IsActive == false)
                .Select(task => task.ToTaskDto())
                .ToList();

            return listOfTasksDto;
        }

        public IEnumerable<TaskDto> GetAllArchivedTasksByUserId(int userId)
        {
            var listOfTasksDto = UnitOfWork.Tasks.GetAllEntitiesByFilter((task) => (task.UserId == userId && task.IsActive == false))
                .Select(task => task.ToTaskDto())
                .ToList();

            return listOfTasksDto;
        }

        public IEnumerable<TaskDto> ImportTasksFromCsv(IFormFile uploadFile, int userId)
        {
            IEnumerable<TaskDto> listOfTasksDTO = new List<TaskDto>();
            if (uploadFile.Length > 0)
            {
                using (var stream = uploadFile.OpenReadStream())
                {
                    listOfTasksDTO = CsvSerializer.DeserializeFromStream<IEnumerable<TaskDto>>(stream);
                }
            }
            else
            {
                throw new FileNotFoundException();
            }

            return AddTaskToDatabase(listOfTasksDTO, userId);
        }

        public IEnumerable<TaskDto> ImportTasksFromXml(IFormFile uploadFile, int userId)
        {
            IEnumerable<TaskDto> listOfTasksDTO = new List<TaskDto>();
            DefaultXmlSerializer xmlSerializer = new DefaultXmlSerializer(listOfTasksDTO.GetType());
            if (uploadFile.Length > 0)
            {
                using (var stream = uploadFile.OpenReadStream())
                {
                    listOfTasksDTO = (IEnumerable<TaskDto>)xmlSerializer.Deserialize(stream);
                }
            }
            else
            {
                throw new FileNotFoundException();
            }

            return AddTaskToDatabase(listOfTasksDTO, userId);
        }
    }
}
