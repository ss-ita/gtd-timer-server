//-----------------------------------------------------------------------
// <copyright file="TaskRecordDtoExtension.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using GtdCommon.ModelsDto;
using GtdTimerDAL.Entities;

namespace GtdTimerDAL.Extensions
{
    /// <summary>
    /// TaskRecordDtoExtension class for converting to record and vice versa
    /// </summary>
    public static class TaskRecordDTOExtension
    {
        /// <summary>
        /// Convert to TaskRecord method
        /// </summary>
        /// <param name="record">Record variable</param>
        /// <param name="task">Task variable</param>
        /// <returns>Returns TaskRecordDto model</returns>
        public static TaskRecordDto ToTaskRecord(this Record record)
        {
            TaskRecordDto recordToReturn = new TaskRecordDto
            {
                Id = record.Id,
                Name = record.Name,
                Description = record.Description,
                Action = record.Action,
                StartTime = record.StartTime,
                StopTime = record.StopTime,
                TaskId = record.TaskId,
                ElapsedTime = record.ElapsedTime,
                WatchType = record.WatchType,
                UserId = record.UserId
            };

            return recordToReturn;
        }

        /// <summary>
        /// Convert to record method
        /// </summary>
        /// <param name="record">TaskRecordDto model</param>
        /// <returns>Returns Record model</returns>
        public static Record ToRecord(this TaskRecordDto record)
        {
            Record recordToReturn = new Record
            {
                Id = record.Id,
                Name = record.Name,
                Description = record.Description,
                Action = record.Action,
                StartTime = record.StartTime,
                StopTime = record.StopTime,
                TaskId = record.TaskId,
                ElapsedTime = record.ElapsedTime,
                WatchType = record.WatchType,
                UserId = record.UserId
            };
           
            return recordToReturn;
        }
    }
}
