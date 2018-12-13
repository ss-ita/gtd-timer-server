using Microsoft.AspNetCore.Mvc;

using ServiceTier.Services;
using Common.Attributes;
using Common.Extentions.Exceptions;
using gtdtimer.Timer.DTO;
using Common.Attributes;

namespace gtdtimer.Controllers
{
    [Route("api/[controller]")]
    public class SignUpController : ControllerBase
    {
        private readonly ISignUpService signUpService;

        public SignUpController(ISignUpService signUpService)
        {
            this.signUpService = signUpService;
        }

        /// <summary>
        /// Retrieve user by his/her Id.
        /// </summary>
        /// <param name="id">The Id of the desired User</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var user = signUpService.GetUserById(id);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return Ok(user);
        }

        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="model">The DTO model of User entity</param>
        /// <returns></returns>
        [ValidateModel]
        [UserAlreadyExistsExceptionFilter]
        [HttpPost]
        public ActionResult Post([FromBody]UserDTO model)
        {
            signUpService.AddUser(model);

            return Ok();
        }
    }
}

