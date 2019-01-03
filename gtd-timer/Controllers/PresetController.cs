using Microsoft.AspNetCore.Mvc;

using ServiceTier.Services;
using Common.ModelsDTO;
using Microsoft.AspNetCore.Authorization;

namespace gtdtimer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PresetController : ControllerBase
    {
        private readonly IUserIdentityService userIdentityService;
        private readonly IPresetService presetService;

        public PresetController(IUserIdentityService userIdentityService, IPresetService presetService)
        {
            this.userIdentityService = userIdentityService;
            this.presetService = presetService;
        }

        [HttpGet("[action]")]
        public IActionResult GetPreset(int presetid)
        {
            var preset = presetService.GetPresetById(presetid);

            return Ok(preset);
        }

        [HttpGet("[action]")]
        public IActionResult GetAllCustomPresets()
        {
            var userid = userIdentityService.GetUserId();
            var query = presetService.GetAllCustomPresets(userid);

            return Ok(query);
        }

        [HttpGet("[action]")]
        public IActionResult GetAllStandardPresets()
        {
            var query = presetService.GetAllStandardPresets();

            return Ok(query);
        }

        [HttpPost("[action]")]
        public IActionResult Post([FromBody]PresetDTO presetDTO)
        {
            presetService.CreatePreset(presetDTO);

            return Ok();
        }

        [HttpPut("[action]")]
        public IActionResult UpdatePreset([FromBody]PresetDTO presetDTO)
        {
            presetService.UpdatePreset(presetDTO);

            return Ok();
        }

        [HttpPut("[action]")]
        public IActionResult UpdateTimer([FromBody]TimerDTO timerDTO)
        {
            presetService.UpdateTimer(timerDTO);

            return Ok();
        }

        [HttpDelete("[action]")]
        public IActionResult DeletePreset(int presetid)
        {
            presetService.DeletePresetById(presetid);

            return Ok();
        }

        [HttpDelete("[action]")]
        public IActionResult DeleteTimer(int timerid)
        {
            presetService.DeleteTimer(timerid);

            return Ok();
        }
    }
}
