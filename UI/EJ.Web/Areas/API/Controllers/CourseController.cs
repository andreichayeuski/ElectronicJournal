using EJ.Domain.Services;
using EJ.Models.UI;
using Microsoft.AspNetCore.Mvc;

namespace EJ.Web.Areas.API.Controllers
{
    [Route("API/Course")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        // GET: api/course
        [HttpGet]
        public JsonResult Get()
        {
            return Json(_courseService.GetCourses());
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            return Json(_courseService.GetCourse(id));
        }

        [HttpPost]
        public JsonResult Post([FromBody]CourseUi course)
        {
            return Json(_courseService.AddCourse(course));
        }

        [HttpPut("{id}")]
        public JsonResult Put(int id, [FromBody]CourseUi value)
        {
            return Json(_courseService.UpdateCourse(id, value));
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            return Json(_courseService.DeleteCourse(id));
        }
    }
}
