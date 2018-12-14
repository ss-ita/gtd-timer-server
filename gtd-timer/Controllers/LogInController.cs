using Microsoft.AspNetCore.Mvc;

using gtdtimer.Attributes;
using Common.Model;
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
        public IActionResult Login([FromBody] LoginModel model)
        {
            var token = logInService.CreateToken(model);

            return Ok(token);
        }
    }
}
