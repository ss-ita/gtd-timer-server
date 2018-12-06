using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gtdtimer.Timer.DAL.Entities;
using gtdtimer.Timer.DAL.UnitOfWork;
using gtdtimer.Timer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gtdtimer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private IUnitOfWork unitOfWork;/* = new UnitOfWork(new TimerContext(new DbContextOptions<TimerContext>()));*/

        public SignUpController(IUnitOfWork uow)
        {
            this.unitOfWork = uow;
        }

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