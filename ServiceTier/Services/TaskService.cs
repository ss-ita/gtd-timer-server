using System;
using System.Collections.Generic;
using System.Linq;
using Timer.DAL.Extensions;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.UnitOfWork;

using Common.Exceptions;
using Common.ModelsDTO;

namespace ServiceTier.Services
{
    public class TaskService : BaseService, ITaskService
    {
        public TaskService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public void CreateTask(TaskDTO taskDTO)
        {
            var task = taskDTO.ToTask();
            unitOfWork.Tasks.Create(task);
            unitOfWork.Save();
        }

        public void DeleteTaskById(int taskId)
        {
            var toDelete = unitOfWork.Tasks.GetByID(taskId);
            if (toDelete != null)
            {
                unitOfWork.Tasks.Delete(toDelete);
                unitOfWork.Save();
            }
            else
            {
                throw new TaskNotFoundException();
            }
        }

        public IEnumerable<TaskDTO> GetAllTasks()
        {
            var listOfTasksDTO = unitOfWork.Tasks.GetAllEntities().Select(task => task.ToTaskDTO()).ToList();

            return listOfTasksDTO;
        }

        public IEnumerable<TaskDTO> GetAllTasksByUserId(int userId)
        {
            var listOfTasksDTO = unitOfWork.Tasks.GetAllEntitiesByFilter(user => user.UserId == userId).Select(task => task.ToTaskDTO()).ToList();

            return listOfTasksDTO;
        }

        public TaskDTO GetTaskById(int taskId)
        {
            Tasks task = unitOfWork.Tasks.GetByID(taskId);
            if (task == null)
            {
                throw new TaskNotFoundException();
            }

            return task.ToTaskDTO();
        }

        public void UpdateTask(TaskDTO taskDTO)
        {
            var task = taskDTO.ToTask();

            unitOfWork.Tasks.Update(task);
            unitOfWork.Save();
        }

        public void SwitchArchivedStatus(TaskDTO model)
        {
            model.IsActive = !model.IsActive;

            var task = model.ToTask();

            unitOfWork.Tasks.Update(task);
            unitOfWork.Save();
        }

        public void ResetTask(int taskId)
        {
            var task = unitOfWork.Tasks.GetByID(taskId);
            if (task == null)
            {
                throw new TaskNotFoundException();
            }

            task.ElapsedTime = TimeSpan.Zero;
            task.Goal = null;
            task.LastStartTime = task.LastStartTime.Date;
            task.IsRunning = false;

            unitOfWork.Tasks.Update(task);
            unitOfWork.Save();
        }

        public void StartTask(TaskDTO model)
        {
            model.IsRunning = true;
            var task = model.ToTask();

            unitOfWork.Tasks.Update(task);
            unitOfWork.Save();
        }

        public void PauseTask(TaskDTO model)
        {
            model.IsRunning = false;
            var task = model.ToTask();

            unitOfWork.Tasks.Update(task);
            unitOfWork.Save();
        }

        public IEnumerable<TaskDTO> GetAllActiveTasks()
        {
            var listOfTasksDTO = unitOfWork.Tasks.GetAllEntitiesByFilter(task => task.IsActive == true).Select(task => task.ToTaskDTO()).ToList();

            return listOfTasksDTO;
        }

        public IEnumerable<TaskDTO> GetAllActiveTasksByUserId(int userId)
        {
            var listOfTasksDTO = unitOfWork.Tasks.GetAllEntitiesByFilter((task) => (task.UserId == userId && task.IsActive == true)).Select(task => task.ToTaskDTO()).ToList();

            return listOfTasksDTO;
        }

        public IEnumerable<TaskDTO> GetAllArchivedTasks()
        {
            var listOfTasksDTO = unitOfWork.Tasks.GetAllEntitiesByFilter(task => task.IsActive == false).Select(task => task.ToTaskDTO()).ToList();

            return listOfTasksDTO;
        }

        public IEnumerable<TaskDTO> GetAllArchivedTasksByUserId(int userId)
        {
            var listOfTasksDTO = unitOfWork.Tasks.GetAllEntitiesByFilter((task) => (task.UserId == userId && task.IsActive == false)).Select(task => task.ToTaskDTO()).ToList();

            return listOfTasksDTO;
        }
    }
}
