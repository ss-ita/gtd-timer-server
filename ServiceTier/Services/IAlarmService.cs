//-----------------------------------------------------------------------
// <copyright file="IAlarmService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

using GtdCommon.ModelsDto;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// interface for alarm service class
    /// </summary>
    public interface IAlarmService : IBaseService
    {
        /// <summary>
        /// Method for getting all alrms of chosen user
        /// </summary>
        /// <param name="userId">id of chosen user</param>
        /// <returns>all alarms of chosen user</returns>
        IEnumerable<AlarmDto> GetAllAlarmsByUserId(int userId);

        /// <summary>
        /// Method for creating a alarm
        /// </summary>
        /// <param name="alarmDto">alarm model</param>
        void CreateAlarm(AlarmDto alarmDto);

        /// <summary>
        /// Method for updating a alarm
        /// </summary>
        /// <param name="alarmDto">alarm model</param>
        void UpdateAlarm(AlarmDto alarmDto);

        /// <summary>
        /// Method for deleting a alarm by id
        /// </summary>
        /// <param name="alarmId">id of chosen alarm</param>
        void DeleteAlarmById(int alarmId);
    }
}
