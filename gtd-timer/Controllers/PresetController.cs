using Microsoft.AspNetCore.Mvc;

using ServiceTier.Services;
using Common.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Common.Exceptions;

namespace gtdtimer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PresetController : ControllerBase
    {
        private readonly IUserIdentityService userIdentityService;
        private readonly IPresetService presetService;
        private readonly ITimerService timerService;

        public PresetController(IUserIdentityService userIdentityService, IPresetService presetService, ITimerService timerService)
        {
            this.userIdentityService = userIdentityService;
            this.presetService = presetService;
            this.timerService = timerService;
        }

        [HttpGet("GetPreset/{presetid}")]
        public IActionResult GetPreset(int presetid)
        {
            var preset = presetService.GetPresetById(presetid);

            return Ok(preset);
        }

        [HttpGet("[action]")]
        public IActionResult GetAllCustomPresets()
        {
            var userid = userIdentityService.GetUserId();
            var query = presetService.GetAllCustomPresetsByUserId(userid);

            return Ok(query);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult GetAllStandardPresets()
        {
            var query = presetService.GetAllStandardPresets();

            return Ok(query);
        }

        [HttpPost("[action]")]
        public IActionResult CreatePreset([FromBody]PresetDTO presetDTO)
        {
            presetDTO.UserId = userIdentityService.GetUserId();
            presetService.CreatePreset(presetDTO);

            return Ok();
        }

        [HttpPut("[action]")]
        public IActionResult UpdatePreset([FromBody]PresetDTO presetDTO)
        {
            presetDTO.UserId = userIdentityService.GetUserId();
            presetService.UpdatePreset(presetDTO);

            return Ok();
        }

        [HttpPut("[action]")]
        public IActionResult UpdateTimer([FromBody]TimerDTO timerDTO)
        {
            timerService.UpdateTimer(timerDTO);

            return Ok();
        }

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

        [HttpDelete("DeleteTimer/{timerid}")]
        public IActionResult DeleteTimer(int timerid)
        {
            timerService.DeleteTimer(timerid);

            return Ok();
        }
    }
}
