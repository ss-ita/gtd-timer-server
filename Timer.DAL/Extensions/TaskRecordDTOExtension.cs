using System;

using GtdCommon.ModelsDto;
using GtdTimerDAL.Entities;

namespace GtdTimerDAL.Extensions
{
    public static class TaskRecordDTOExtension
    {
        public static TaskRecordDto ToTaskRecord(this Record record, Tasks task)
        {
            TaskRecordDto recordToReturn = new TaskRecordDto
            {
                Id = record.Id,
                Name = task.Name,
                Description = task.Description,
                Action = record.Action,
                StartTime = record.StartTime,
                StopTime = record.StopTime,
                TaskId = task.Id
            };

            return recordToReturn;
        }

        public static Record ToRecord(this TaskRecordDto record)
        {
            Record recordToReturn = new Record
            {
                Id = record.Id,
               
                Action = record.Action,
                StartTime = record.StartTime,
                StopTime = record.StopTime,
                TaskId = record.TaskId
            };

            return recordToReturn;
        }
    }
}
