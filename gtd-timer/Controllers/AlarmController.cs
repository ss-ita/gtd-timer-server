//-----------------------------------------------------------------------
// <copyright file="AlarmController.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using GtdServiceTier.Services;
using GtdTimer.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using GtdCommon.ModelsDto;

namespace GtdTimer.Controllers
{
    /// <summary>
    /// class for alarm controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class AlarmController : ControllerBase
    {
        /// <summary>
        /// instance of user identity service
        /// </summary>
        private readonly IUserIdentityService userIdentityService;

        /// <summary>
        /// instance of alarm service
        /// </summary>
        private readonly IAlarmService alarmService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlarmController" /> class.
        /// </summary>
        /// <param name="alarmService">instance of alarm service</param>
        /// <param name="userIdentityService">instance of user identity service</param>
        public AlarmController(IAlarmService alarmService, IUserIdentityService userIdentityService)
        {
            this.alarmService = alarmService;
            this.userIdentityService = userIdentityService;
        }

        /// <summary>
        /// Returns all user's alarms.
        /// </summary>
        /// <returns>result of getting all user's alarms.</returns>
        [HttpGet("[action]")]
        public IActionResult GetAllAlarmsByUserId()
        {
            var userId = this.userIdentityService.GetUserId();
            var alarms = this.alarmService.GetAllAlarmsByUserId(userId);

            return Ok(alarms);
        }

        /// <summary>
        /// Creates a alarm.
        /// </summary>
        /// <param name="model">alarm model</param>
        /// <returns>result model of creating alarm</returns>
        [ValidateModel]
        [HttpPost("[action]")]
        public IActionResult CreateAlarm([FromBody]AlarmDto model)
        {
            model.UserId = this.userIdentityService.GetUserId();
            alarmService.CreateAlarm(model);

            return Ok(model);
        }

        /// <summary>
        /// Updates the alarm.
        /// </summary>
        /// <param name="model">alarm model</param>
        /// <returns>result of updating the alarm</returns>
        [ValidateModel]
        [HttpPut("[action]")]
        public IActionResult UpdateAlarm([FromBody]AlarmDto model)
        {
            model.UserId = this.userIdentityService.GetUserId();
            this.alarmService.UpdateAlarm(model);

            return Ok(model);
        }

        /// <summary>
        /// Deletes the alarm by id.
        /// </summary>
        /// <param name="alarmId">id of chosen alarm</param>
        /// <returns>result of deleting the alarm by id.</returns>
        [HttpDelete("DeleteAlarm/{alarmId}")]
        public IActionResult DeleteAlarm(int alarmId)
        {
            this.alarmService.DeleteAlarmById(alarmId);

            return this.Ok();
        }
    }
}