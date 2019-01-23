using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Common.Exceptions;
using Common.ModelsDTO;
using DefaultXmlSerializer = System.Xml.Serialization.XmlSerializer;
using ServiceStack.Text;
using Timer.DAL.Extensions;
using Timer.DAL.Timer.DAL.Entities;
using Timer.DAL.Timer.DAL.UnitOfWork;

namespace ServiceTier.Services
{
    public class TaskService: BaseService, ITaskService
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

        public IEnumerable<TaskDTO> AddTaskToDatabase(IEnumerable<TaskDTO> listOfTasksDto, int userId)
        {
            var newTasks = new List<Tasks>();

            foreach(var taskDto in listOfTasksDto)
            {
                taskDto.UserId = userId;
                var task = taskDto.ToTask();
                unitOfWork.Tasks.Create(task);
                newTasks.Add(task);
            }

            unitOfWork.Save();

            return newTasks.Select(task => task.ToTaskDTO());
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
            var listOfTasksDTO = unitOfWork.Tasks.GetAllEntities()
                .Select(task => task.ToTaskDTO())
                .ToList();

            return listOfTasksDTO;
        }

        public IEnumerable<TaskDTO> GetAllTasksByUserId(int userId)
        {
            var listOfTasksDTO = unitOfWork.Tasks.GetAllEntitiesByFilter(user => user.UserId == userId)
                .Select(task => task.ToTaskDTO())
                .ToList();

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

        public void UpdateTask(TaskDTO model)
        {
            var task = model.ToTask();

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

        public void ResetTask(TaskDTO model)
        {
            model.ElapsedTime = 0;
            model.Goal = null;
            model.LastStartTime = model.LastStartTime.Date;
            model.IsRunning = false;

            var task = model.ToTask();

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
            var listOfTasksDTO = unitOfWork.Tasks.GetAllEntitiesByFilter(task => task.IsActive == true)
                .Select(task => task.ToTaskDTO())
                .ToList();

            return listOfTasksDTO;
        }

        public IEnumerable<TaskDTO> GetAllActiveTasksByUserId(int userId)
        {
            var listOfTasksDTO = unitOfWork.Tasks.GetAllEntitiesByFilter((task) => (task.UserId == userId && task.IsActive == true))
                .Select(task => task.ToTaskDTO())
                .ToList();

            return listOfTasksDTO;
        }

        public IEnumerable<TaskDTO> GetAllArchivedTasks()
        {
            var listOfTasksDTO = unitOfWork.Tasks.GetAllEntitiesByFilter(task => task.IsActive == false)
                .Select(task => task.ToTaskDTO())
                .ToList();

            return listOfTasksDTO;
        }

        public IEnumerable<TaskDTO> GetAllArchivedTasksByUserId(int userId)
        {
            var listOfTasksDTO = unitOfWork.Tasks.GetAllEntitiesByFilter((task) => (task.UserId == userId && task.IsActive == false))
                .Select(task => task.ToTaskDTO())
                .ToList();

            return listOfTasksDTO;
        }

        public IEnumerable<TaskDTO> ImportTasksFromCsv(IFormFile uploadFile, int userId)
        {
            IEnumerable<TaskDTO> listOfTasksDTO = new List<TaskDTO>();
            if (uploadFile.Length > 0)
            {
                using (var stream = uploadFile.OpenReadStream())
                {
                    listOfTasksDTO = CsvSerializer.DeserializeFromStream<IEnumerable<TaskDTO>>(stream);
                }
            }
            else
            {
                throw new FileNotFoundException();
            }

            return AddTaskToDatabase(listOfTasksDTO, userId);
        }

        public IEnumerable<TaskDTO> ImportTasksFromXml(IFormFile uploadFile, int userId)
        {
            IEnumerable<TaskDTO> listOfTasksDTO = new List<TaskDTO>();
            DefaultXmlSerializer xmlSerializer = new DefaultXmlSerializer(listOfTasksDTO.GetType());
            if (uploadFile.Length > 0)
            {
                using (var stream = uploadFile.OpenReadStream())
                {
                    listOfTasksDTO = (IEnumerable<TaskDTO>)xmlSerializer.Deserialize(stream);
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
