using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EJ.Domain.Services;
using EJ.Models.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EJ.Web.Areas.API.Controllers
{
    [Route("API/User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/User
        [HttpGet]
        public JsonResult Get()
        {
            return Json(_userService.GetUsers());
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public JsonResult Get(int id)
        {
            return Json(_userService.GetUser(id));
        }

        // POST: api/User
        [HttpPost]
        public JsonResult Post([FromBody] UserInfoViewModel value)
        {
            return Json(_userService.AddUser(value));
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public JsonResult Put(int id, [FromBody] UserInfoViewModel value)
        {
            return Json(_userService.UpdateUser(id, value));
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            return Json(_userService.DeleteUser(id));
        }
    }
}
