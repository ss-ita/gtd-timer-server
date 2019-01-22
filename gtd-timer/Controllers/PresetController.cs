//-----------------------------------------------------------------------
// <copyright file="PresetController.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using GtdCommon.Exceptions;
using GtdCommon.ModelsDto;
using GtdServiceTier.Services;

namespace GtdTimer.Controllers
{
    /// <summary>
    /// class for preset controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class PresetController : ControllerBase
    {
        /// <summary>
        /// instance of user identity service
        /// </summary>
        private readonly IUserIdentityService userIdentityService;

        /// <summary>
        /// instance of preset service
        /// </summary>
        private readonly IPresetService presetService;

        /// <summary>
        /// instance of timer service
        /// </summary>
        private readonly ITimerService timerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PresetController" /> class.
        /// </summary>
        /// <param name="userIdentityService">instance of user identity service</param>
        /// <param name="presetService">instance of preset service</param>
        /// <param name="timerService">instance of timer service</param>
        public PresetController(IUserIdentityService userIdentityService, IPresetService presetService, ITimerService timerService)
        {
            this.userIdentityService = userIdentityService;
            this.presetService = presetService;
            this.timerService = timerService;
        }

        /// <summary>
        /// Method for obtaining a preset by id
        /// </summary>
        /// <param name="presetid"> id of preset</param>
        /// <returns> result of getting preset with chosen id </returns>
        [HttpGet("GetPreset/{presetid}")]
        public IActionResult GetPreset(int presetid)
        {
            var preset = this.presetService.GetPresetById(presetid);

            return this.Ok(preset);
        }

        /// <summary>
        /// Method for getting all custom presets of chosen user
        /// </summary>
        /// <returns> result of getting a list of all custom presets of chosen user </returns>
        [HttpGet("[action]")]
        public IActionResult GetAllCustomPresets()
        {
            var userid = this.userIdentityService.GetUserId();
            var query = this.presetService.GetAllCustomPresetsByUserId(userid);

            return this.Ok(query);
        }

        /// <summary>
        /// Method for getting all standard presets
        /// </summary>
        /// <returns> result of getting a list of all standard presets</returns>
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult GetAllStandardPresets()
        {
            var query = this.presetService.GetAllStandardPresets();

            return this.Ok(query);
        }

        /// <summary>
        /// Method for creating a preset
        /// </summary>
        /// <param name="presetDto"> preset model</param>
        /// <returns> result of creating a preset</returns>
        [HttpPost("[action]")]
        public IActionResult CreatePreset([FromBody]PresetDto presetDto)
        {
            presetDto.UserId = this.userIdentityService.GetUserId();
            this.presetService.CreatePreset(presetDto);

            return this.Ok();
        }

        /// <summary>
        /// Method for updating a preset
        /// </summary>
        /// <param name="presetDto"> preset model</param>
        /// <returns> result of updating a preset</returns>
        [HttpPut("[action]")]
        public IActionResult UpdatePreset([FromBody]PresetDto presetDto)
        {
            presetDto.UserId = this.userIdentityService.GetUserId();
            this.presetService.UpdatePreset(presetDto);

            return this.Ok();
        }

        /// <summary>
        /// Method for updating a timer
        /// </summary>
        /// <param name="timerDto">timer model</param>
        /// <returns> result of updating a timer</returns>
        [HttpPut("[action]")]
        public IActionResult UpdateTimer([FromBody]TimerDto timerDto)
        {
            this.timerService.UpdateTimer(timerDto);

            return this.Ok();
        }

        /// <summary>
        /// Method for deleting a preset
        /// </summary>
        /// <param name="presetid"> id of preset</param>
        /// <returns> result of deleting a preset</returns>
        [HttpDelete("DeletePreset/{presetid}")]
        public IActionResult DeletePreset(int presetid)
        {
            if ((this.presetService.GetPresetById(presetid).UserId == null) || (this.presetService.GetPresetById(presetid).UserId != this.userIdentityService.GetUserId()))
            {
                throw new AccessDeniedException();
            }

            this.presetService.DeletePresetById(presetid);

            return this.Ok();
        }

        /// <summary>
        /// Method for deleting a timer
        /// </summary>
        /// <param name="timerid">id of chosen timer</param>
        /// <returns> result of deleting a timer</returns>
        [HttpDelete("DeleteTimer/{timerid}")]
        public IActionResult DeleteTimer(int timerid)
        {
            this.timerService.DeleteTimer(timerid);

            return this.Ok();
        }
    }
}
