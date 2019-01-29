//-----------------------------------------------------------------------
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
using GtdCommon.Constant;
using System;

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
            taskDto.Id = task.Id;
        }

        public IEnumerable<TaskDto> AddTaskToDatabase(IEnumerable<TaskDto> listOfTasksDto, int userId)
        {
            var newTasks = new List<Tasks>();

            foreach (var taskDto in listOfTasksDto)
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

        public IEnumerable<TaskDto> GetAllTasksByDate(int userId, DateTime start, DateTime end)
        {
            var listOfTasksDto = UnitOfWork.Tasks.GetAllEntitiesByFilter((task) =>
            ((task.UserId == userId) &&
            (task.LastStartTime >= start && task.LastStartTime <= end)))
            .Select(task => task.ToTaskDto())
            .ToList();

            return listOfTasksDto;
        }

        public IEnumerable<TaskDto> ImportTasksFromCsv(IFormFile uploadFile, int userId)
        {
            IEnumerable<TaskDto> listOfTasksDTO = new List<TaskDto>();

            try
            {
                using (var stream = uploadFile.OpenReadStream())
                {
                    listOfTasksDTO = CsvSerializer.DeserializeFromStream<IEnumerable<TaskDto>>(stream);
                }
            }
            catch (Exception e)
            {
                throw new ImportErrorException(e.Message);
            }

            return AddTaskToDatabase(listOfTasksDTO, userId);
        }

        public IEnumerable<TaskDto> ImportTasksFromXml(IFormFile uploadFile, int userId)
        {
            IEnumerable<TaskDto> listOfTasksDTO = new List<TaskDto>();
            DefaultXmlSerializer xmlSerializer = new DefaultXmlSerializer(listOfTasksDTO.GetType());

            try
            {
                using (var stream = uploadFile.OpenReadStream())
                {
                    listOfTasksDTO = (IEnumerable<TaskDto>)xmlSerializer.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                throw new ImportErrorException(e.Message);
            }

            return AddTaskToDatabase(listOfTasksDTO, userId);
        }

        public IEnumerable<TaskRecordDto> GetAllRecordsByUserId(int userId)
        {

            var listOfTasks = this.UnitOfWork.Tasks.GetAllEntitiesByFilter(task => task.UserId == userId);
            var listOfTaskRecords = (from tasks in listOfTasks
                                     from taskRecord in UnitOfWork.Records.GetAllEntitiesByFilter(record=>record.TaskId==tasks.Id)
                                     select new TaskRecordDto
                                     {
                                         Id = taskRecord.Id,
                                         TaskId = tasks.Id,
                                         Name = tasks.Name,
                                         Description = tasks.Description,
                                         Action = taskRecord.Action,
                                         StartTime = taskRecord.StartTime,
                                         StopTime = taskRecord.StopTime,
                                         ElapsedTime =taskRecord.ElapsedTime
                                     }).ToList();

            return listOfTaskRecords;    
        }

        public void CreateRecord(TaskRecordDto taskRecord)
        {
            Record record = taskRecord.ToRecord();
            UnitOfWork.Records.Create(record);
            UnitOfWork.Save();
        }

        public IEnumerable<TaskRecordDto> GetAllRecordsByTaskId(int userId,int taskId)
        {
            var listOfRecords = (UnitOfWork.Records.GetAllEntitiesByFilter(record=>record.TaskId == taskId)
                .Where(record => record.TaskId == taskId)
                .Select(record => record.ToTaskRecord(UnitOfWork.Tasks.GetByID(taskId))))
                .ToList();

            return listOfRecords;

        }

        public void DeleteRecordById(int taskId)
        {
            var toDelete =  UnitOfWork.Records.GetByID(taskId);
            if (toDelete != null)
            {
                UnitOfWork.Records.Delete(toDelete);
                UnitOfWork.Save();
            }
            else
            {
                throw new TaskNotFoundException();
            }
        }
        public List<TaskDto> GetAllTasksByPresetId(int presetid)
        {
            return UnitOfWork.PresetTasks.GetAllEntitiesByFilter(task => task.PresetId == presetid)
                .Select(tasks => UnitOfWork.Tasks.GetByID(tasks.Id).ToTaskDto()).ToList();
        }

        public IEnumerable<TaskDto> GetAllTimersByUserId(int userId)
        {
            var listOfTasksDto = UnitOfWork.Tasks.GetAllEntitiesByFilter((task) => (task.UserId == userId && task.WatchType == WatchType.Timer))
                .Select(task => task.ToTaskDto())
                .ToList();

            return listOfTasksDto;
        }

        public IEnumerable<TaskDto> GetAllStopwatchesByUserId(int userId)
        {
            var listOfTasksDto = UnitOfWork.Tasks.GetAllEntitiesByFilter((task) => (task.UserId == userId && task.WatchType == WatchType.Stopwatch))
                .Select(task => task.ToTaskDto())
                .ToList();

            return listOfTasksDto;
        }
    }
}
