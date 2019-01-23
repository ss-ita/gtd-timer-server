//-----------------------------------------------------------------------
// <copyright file="ITimerService.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;

using GtdCommon.ModelsDto;

namespace GtdServiceTier.Services
{
    /// <summary>
    /// Interface for Timer service class
    /// </summary>
    public interface ITimerService : IBaseService
    {
        /// <summary>
        /// Method for creating a timer
        /// </summary>
        /// <param name="timerDto">timer model</param>
        void CreateTimer(TimerDto timerDto);

        /// <summary>
        /// Method for updating a timer
        /// </summary>
        /// <param name="timerDto">timer model</param>
        void UpdateTimer(TimerDto timerDto);

        /// <summary>
        /// Method for deleting a timer
        /// </summary>
        /// <param name="timerid">id of chosen timer</param>
        void DeleteTimer(int timerid);

        /// <summary>
        /// Method for getting all timers by preset id
        /// </summary>
        /// <param name="presetid">id of chosen preset</param>
        /// <returns>list of timers of chosen preset</returns>
        List<TimerDto> GetAllTimersByPresetId(int presetid);
    }
}
