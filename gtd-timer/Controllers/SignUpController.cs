using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


using gtdtimer.Attributes;
using ServiceTier.Services;
using Common.Exceptions;
using gtdtimer.ModelsDTO;
using Timer.DAL.Timer.DAL.UnitOfWork;
using Timer.DAL.Timer.DAL.Entities;

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
        /// Create user.
        /// </summary>
        /// <param name="model">The DTO model of User entity</param>
        /// <returns></returns>
        [ValidateModel]
        [HttpPost]
        public ActionResult Post([FromBody]UserDTO model)
        {
            signUpService.AddUser(model);

            return Ok();
        }
    }
}

