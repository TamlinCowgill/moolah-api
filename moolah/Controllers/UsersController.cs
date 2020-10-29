using System;
using System.Linq;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using moolah.Services;

namespace moolah.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var task = _userService.GetAll();
            return Ok(task.Result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser(string id)
        {
            var task = _userService.GetUser(id);
            if (task.Result == null)
            {
                return NotFound(nameof(id));
            }

            return Ok(task.Result);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] string name)
        {
            var task = _userService.CreateUser(name);

            return Ok(task.Result);
        }

    }
}