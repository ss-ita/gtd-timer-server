//-----------------------------------------------------------------------
// <copyright file="LogInController.cs" company="SoftServe">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;

using GtdCommon.ModelsDto;
using GtdTimer.Attributes;
using GtdServiceTier.Services;

namespace GtdTimer.Controllers
{
    /// <summary>
    /// class for log in controller
    /// </summary>
    [Route("api/[controller]")]
    [ValidateModel]
    public class LogInController : ControllerBase
    {
        /// <summary>
        /// instance of log in service
        /// </summary>
        private readonly ILogInService logInService;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogInController" /> class.
        /// </summary>
        /// <param name="logInService">instance of log in service</param>
        public LogInController(ILogInService logInService)
        {
            this.logInService = logInService;
        }

        /// <summary>
        /// Method for logging in
        /// </summary>
        /// <param name="model">log in model</param>
        /// <returns>result of logging in</returns>
        [HttpPost]
        public IActionResult Login([FromBody] LoginDto model)
        {
            var token = logInService.CreateToken(model);

            return Ok(token);
        }

        /// <summary>
        /// Method for logging in with email
        /// </summary>
        /// <param name="email">user email</param>
        /// <returns>result of logging in</returns>
        [HttpGet("{email}")]
        public IActionResult LoginWithEmail(string email)
        {
            var token = logInService.CreateTokenWithEmail(email);

            return Ok(token);
        }

        /// <summary>
        /// Method for logging in with google
        /// </summary>
        /// <param name="accessToken">model of google token</param>
        /// <returns>result of logging in</returns>
        [HttpPost("[action]")]
        public IActionResult GoogleLogin([FromBody] SocialAuthDto accessToken)
        {
            var token = logInService.CreateTokenWithGoogle(accessToken);

            return Ok(token);
        }

        /// <summary>
        /// Method for logging in with facebook
        /// </summary>
        /// <param name="accessToken">model of facebook token</param>
        /// <returns>result of logging in</returns>
        [HttpPost("[action]")]
        public IActionResult FacebookLogin([FromBody] SocialAuthDto accessToken)
        {
            var token = logInService.CreateTokenWithFacebook(accessToken);

            return Ok(token);
        }
    }
}
