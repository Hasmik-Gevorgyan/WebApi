using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models;
using WebApi.DataAccess;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        // private static readonly string[] Summaries = new[]
        // {
        //     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        // };

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            UserDAL userDAL = new UserDAL();
            List<User> Users = userDAL.GetUsersAsGenericList();
            return Users;
        }

         [HttpPost]
        public ActionResult Post(User user)
        {
            try
            {
                UserDAL userdal = new UserDAL();
                userdal.InsertUser(user);
            }
            catch
            {
                return BadRequest("something went wrong");
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                UserDAL userdal = new UserDAL();
                userdal.DeleteUser(id);
            }
            catch
            {
                return BadRequest("somthing went wrong");
            }
            return Ok();
        }
        [HttpPut]
        public ActionResult Update([FromBody]User user)
        {
            try
            {
                UserDAL userdal = new UserDAL();
                userdal.updateUser(user);
            }
            catch
            {
                return BadRequest("something went wrong");
            }
            return Ok();
        }
    }
}
