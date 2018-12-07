using Microsoft.AspNetCore.Mvc;
using System.Linq;

using gtdtimer.Timer.DAL.Entities;
using gtdtimer.Timer.DAL.UnitOfWork;
using gtdtimer.Timer.DTO;

namespace gtdtimer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private IUnitOfWork unitOfWork;

        public SignUpController(IUnitOfWork uow)
        {
            this.unitOfWork = uow;
        }

        /// <summary>
        /// Retrieve user by his/her Id.
        /// </summary>
        /// <param name="id">The Id of the desired User</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var user = unitOfWork.Users.GetByID(id);
            if (user == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            return Ok(user);
        }

        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="model">The DTO model of User entity</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post(UserDTO model)
        {
            var userExist = unitOfWork.Users.GetAll().Any(u => u.Email == model.Email);
            if (userExist)
            {
                return BadRequest("User with such email address already exist");
            } 

            User user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash=model.Password
            }; 
            unitOfWork.Users.Create(user);
            unitOfWork.Save();

            return Ok();
        }

    }
}