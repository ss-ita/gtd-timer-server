using Microsoft.AspNetCore.Mvc;

using gtdtimer.Attributes;
using Common.ModelsDTO;
using ServiceTier.Services;

namespace gtdtimer.Controllers
{
    [Route("api/[controller]")]
    [ValidateModel]
    public class LogInController : ControllerBase
    {
        private readonly ILogInService logInService;

        public LogInController(ILogInService logInService)
        {
            this.logInService = logInService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO model)
        {
            var token = logInService.CreateToken(model);

            return Ok(token);
        }

        [HttpPost("[action]")]
        public IActionResult GoogleLogin([FromBody] SocialAuthDTO accessToken)
        {
            var token = logInService.CreateTokenWithGoogle(accessToken);

            return Ok(token);
        }

        [HttpPost("[action]")]
        public IActionResult FacebookLogin([FromBody] SocialAuthDTO accessToken)
        {
            var token = logInService.CreateTokenWithFacebook(accessToken);

            return Ok(token);
        }
    }
}
