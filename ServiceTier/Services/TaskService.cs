//-----------------------------------------------------------------------
// <copyright file="TaskService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
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

        public void CreateTask(TaskDto model)
        {
            var task = model.ToTask();
            UnitOfWork.Tasks.Create(task);
            UnitOfWork.Save();
            model.Id = task.Id;
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

        public void UpdateTask(TaskDto model)
        {
            var task = model.ToTask();

            UnitOfWork.Tasks.Update(task);
            UnitOfWork.Save();
        }

        public void ResetTask(TaskDto model)
        {
            model.ElapsedTime = 0;
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
            var listOfTaskRecords = (from taskRecord in UnitOfWork.Records.GetAllEntitiesByFilter(record => record.UserId == userId)
                                     select new TaskRecordDto
                                     {
                                         Id = taskRecord.Id,
                                         TaskId = taskRecord.TaskId,
                                         Name = taskRecord.Name,
                                         Description = taskRecord.Description,
                                         Action = taskRecord.Action,
                                         StartTime = taskRecord.StartTime,
                                         StopTime = taskRecord.StopTime,
                                         ElapsedTime = taskRecord.ElapsedTime,
                                         WatchType = taskRecord.WatchType,
                                         UserId = taskRecord.UserId

                                     }).ToList();

            return listOfTaskRecords;
        }

        public void CreateRecord(TaskRecordDto taskRecord, int userId)
        {
            Record record = taskRecord.ToRecord();
            record.UserId = userId;
            UnitOfWork.Records.Create(record);
            UnitOfWork.Save();
        }

        public IEnumerable<TaskRecordDto> GetAllRecordsByTaskId(int userId, int taskId)
        {
            var listOfRecords = (UnitOfWork.Records.GetAllEntitiesByFilter(record => record.TaskId == taskId)
                .Where(record => record.TaskId == taskId)
                .Select(record => record.ToTaskRecord()))
                .ToList();

            return listOfRecords;

        }

        public void DeleteRecordById(int recordId)
        {
            var toDelete = UnitOfWork.Records.GetByID(recordId);
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

        public List<TaskRecordDto> ResetTaskFromHistory(int recordId)
        {
            var recordToFind = UnitOfWork.Records.GetByID(recordId);
            var taskToUpdate = UnitOfWork.Tasks.GetByID(recordToFind.TaskId);
            if (taskToUpdate != null)
            {
                if (taskToUpdate.IsRunning)
                {
                    var timeNow = DateTime.UtcNow;
                    var ellapsedTime = (timeNow - taskToUpdate.LastStartTime).TotalMilliseconds;
                    Record recordToCreate = new Record
                    {
                        Action = "Reset",
                        Name = taskToUpdate.Name,
                        Description = taskToUpdate.Description,
                        TaskId = taskToUpdate.Id,
                        StartTime = taskToUpdate.LastStartTime,
                        StopTime = timeNow,
                        WatchType = taskToUpdate.WatchType,
                        ElapsedTime = ellapsedTime,
                        UserId = taskToUpdate.UserId
                    };
                    UnitOfWork.Records.Create(recordToCreate);
                    taskToUpdate.ElapsedTime = TimeSpan.FromMilliseconds(0);
                    taskToUpdate.LastStartTime = timeNow;
                    UnitOfWork.Tasks.Update(taskToUpdate);
                    UnitOfWork.Save();
                    var listToReturn = new List<TaskRecordDto>
                    {
                        recordToCreate.ToTaskRecord()
                    };
                    return listToReturn;
                }
                else
                {
                    var timeNow = DateTime.UtcNow;
                    taskToUpdate.ElapsedTime = TimeSpan.FromMilliseconds(0);
                    taskToUpdate.LastStartTime = timeNow;
                    taskToUpdate.IsRunning = true;
                    UnitOfWork.Tasks.Update(taskToUpdate);
                    UnitOfWork.Save();
                    return null;
                }
            }
            else
            {
                Record record = UnitOfWork.Records.GetByID(recordId);
                if (record.WatchType == WatchType.Stopwatch)
                {
                    var taskToCreate = new Tasks
                    {
                        Name = record.Name,
                        Description = record.Description,
                        LastStartTime = DateTime.UtcNow,
                        ElapsedTime = TimeSpan.FromMilliseconds(0),
                        IsRunning = true,
                        WatchType = WatchType.Stopwatch,
                        UserId = record.UserId
                    };
                    UnitOfWork.Tasks.Create(taskToCreate);
                    UnitOfWork.Save();

                    var listOfRecordsWithSameTaskId = UnitOfWork.Records.GetAllEntitiesByFilter(rec => rec.TaskId == record.TaskId).ToList();
                    foreach (var item in listOfRecordsWithSameTaskId)
                    {
                        item.TaskId = taskToCreate.Id;
                    }
                    foreach (var item in listOfRecordsWithSameTaskId)
                    {
                        UnitOfWork.Records.Update(item);
                    }
                    UnitOfWork.Save();
                    var listToReturn = new List<TaskRecordDto>();
                    foreach (var item in listOfRecordsWithSameTaskId)
                    {
                        listToReturn.Add(item.ToTaskRecord());
                    }
                    return listToReturn;
                }
                else
                {
                    var taskToCreate = new Tasks
                    {
                        Name = record.Name,
                        Description = record.Description,
                        LastStartTime = new DateTime(1,1,1,0,0,0,0,DateTimeKind.Utc),
                        ElapsedTime = TimeSpan.FromMilliseconds(0),
                        Goal = new TimeSpan(0,0,0,0,0),
                        IsRunning = false,
                        WatchType = WatchType.Timer,
                        UserId = record.UserId
                    };
                    UnitOfWork.Tasks.Create(taskToCreate);
                    UnitOfWork.Save();

                    var listOfRecordsWithSameTaskId = UnitOfWork.Records.GetAllEntitiesByFilter(rec => rec.TaskId == record.TaskId);
                    foreach (var item in listOfRecordsWithSameTaskId)
                    {
                        item.TaskId = taskToCreate.Id;
                    }
                    foreach (var item in listOfRecordsWithSameTaskId)
                    {
                        UnitOfWork.Records.Update(item);
                    }
                    UnitOfWork.Save();
                    var listToReturn = new List<TaskRecordDto>();
                    foreach (var item in listOfRecordsWithSameTaskId)
                    {
                        listToReturn.Add(item.ToTaskRecord());
                    }
                    return listToReturn;
                }
            }
        }

        public List<TaskDto> GetAllTasksByPresetId(int presetid)
        {
            return UnitOfWork.PresetTasks.GetAllEntitiesByFilter(task => task.PresetId == presetid)
                .Select(tasks => UnitOfWork.Tasks.GetByID(tasks.Id).ToTaskDto()).ToList();
        }

        public IEnumerable<TaskDto> GetAllTimersByUserId(int userId, int start = 0, int length = int.MaxValue)
        {
            var listOfTasksDto = UnitOfWork.Tasks.GetAllEntitiesByFilter((task) => (task.UserId == userId && task.WatchType == WatchType.Timer))
                .Select(task => task.ToTaskDto())
                .Skip(start)
                .Take(length)
                .ToList();

            return listOfTasksDto;
        }

        public IEnumerable<TaskDto> GetAllStopwatchesByUserId(int userId, int start = 0, int length = int.MaxValue)
        {
            var listOfTasksDto = UnitOfWork.Tasks.GetAllEntitiesByFilter((task) => (task.UserId == userId && task.WatchType == WatchType.Stopwatch))
                .Select(task => task.ToTaskDto())
                .Skip(start)
                .Take(length)
                .ToList();

            return listOfTasksDto;
        }

        public int GetAllStopwatchesByUserIdCount(int userId)
        {
            var stopwatchesCount = UnitOfWork.Tasks.GetAllEntitiesByFilter((task) => (task.UserId == userId && task.WatchType == WatchType.Stopwatch))
                .Count();

            return stopwatchesCount;
        }

        public int GetAllTimersByUserIdCount(int userId)
        {
            var timersCount = UnitOfWork.Tasks.GetAllEntitiesByFilter((task) => (task.UserId == userId && task.WatchType == WatchType.Timer))
                .Count();

            return timersCount;
        }

        public void MadeRecordsFKNull(int taskId)
        {
            var listOfRecords = UnitOfWork.Records.GetAllEntitiesByFilter((rec) => rec.TaskId == taskId);
            foreach (var item in listOfRecords)
            {
                item.TaskId = null;
            }
            foreach (var item in listOfRecords)
            {
                UnitOfWork.Records.Update(item);
            }
        }
    }
}
