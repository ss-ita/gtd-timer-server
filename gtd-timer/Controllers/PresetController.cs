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
        /// instance of task service
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PresetController" /> class.
        /// </summary>
        /// <param name="userIdentityService">instance of user identity service</param>
        /// <param name="presetService">instance of preset service</param>
        /// <param name="taskService">instance of task service</param>
        public PresetController(IUserIdentityService userIdentityService, IPresetService presetService, ITaskService taskService)
        {
            this.userIdentityService = userIdentityService;
            this.presetService = presetService;
            this.taskService = taskService;
        }

        /// <summary>
        /// Method for obtaining a preset by id
        /// </summary>
        /// <param name="presetid"> id of preset</param>
        /// <returns> result of getting preset with chosen id </returns>
        [HttpGet("GetPreset/{presetid}")]
        public IActionResult GetPreset(int presetid)
        {
            var preset = presetService.GetPresetById(presetid);

            return Ok(preset);
        }

        /// <summary>
        /// Method for getting all custom presets of chosen user
        /// </summary>
        /// <returns> result of getting a list of all custom presets of chosen user </returns>
        [HttpGet("[action]")]
        public IActionResult GetAllCustomPresets()
        {
            var userid = userIdentityService.GetUserId();
            var query = presetService.GetAllCustomPresetsByUserId(userid);

            return Ok(query);
        }

        /// <summary>
        /// Method for getting all standard presets
        /// </summary>
        /// <returns> result of getting a list of all standard presets</returns>
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult GetAllStandardPresets()
        {
            var listOfPresets = presetService.GetAllStandardPresets();

            return Ok(listOfPresets);
        }

        /// <summary>
        /// Method for creating a preset
        /// </summary>
        /// <param name="presetDto"> preset model</param>
        /// <returns> result of creating a preset</returns>
        [HttpPost("[action]")]
        public IActionResult CreatePreset([FromBody]PresetDto presetDto)
        {
            presetDto.UserId = userIdentityService.GetUserId();
            if (presetDto.Tasks != null)
            {
                foreach (var task in presetDto.Tasks)
                {
                    task.UserId = userIdentityService.GetUserId();
                }
            }

            presetService.CreatePreset(presetDto);

            return Ok(presetDto);
        }

        /// <summary>
        /// Method for updating a preset
        /// </summary>
        /// <param name="presetDto"> preset model</param>
        /// <returns> result of updating a preset</returns>
        [HttpPut("[action]")]
        public IActionResult UpdatePreset([FromBody]PresetDto presetDto)
        {
            presetDto.UserId = userIdentityService.GetUserId();
            presetService.UpdatePreset(presetDto);

            return Ok();
        }

        /// <summary>
        /// Method for deleting a preset
        /// </summary>
        /// <param name="presetid"> id of preset</param>
        /// <returns> result of deleting a preset</returns>
        [HttpDelete("DeletePreset/{presetid}")]
        public IActionResult DeletePreset(int presetid)
        {
            if ((presetService.GetPresetById(presetid).UserId == null) || (presetService.GetPresetById(presetid).UserId != userIdentityService.GetUserId()))
            {
                throw new AccessDeniedException();
            }

            presetService.DeletePresetById(presetid);

            return Ok();
        }
    }
}
