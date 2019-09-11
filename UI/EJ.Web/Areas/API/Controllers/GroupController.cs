using EJ.Domain.Services;
using EJ.Models.UI;
using Microsoft.AspNetCore.Mvc;

namespace EJ.Web.Areas.API.Controllers
{
    [Route("API/Group")]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        // GET: api/group
        [HttpGet]
        public JsonResult Get()
        {
            return Json(_groupService.GetGroups());
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return Json(_groupService.GetGroup(id));
        }

        [HttpPost]
        public JsonResult Post([FromBody]GroupUi group)
        {
            return Json(_groupService.AddGroup(group));
        }

        [HttpPut("{id}")]
        public JsonResult Put(int id, [FromBody]GroupUi value)
        {
            return Json(_groupService.UpdateGroup(id, value));
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            return Json(_groupService.DeleteGroup(id));
        }
    }
}
