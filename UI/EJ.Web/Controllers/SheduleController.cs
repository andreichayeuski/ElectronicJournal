using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EJ.Domain.Services;
using EJ.Models.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EJ.Web.Controllers
{
    public class SheduleController : Controller
    {
        private readonly ISheduleService _sheduleService;
        private readonly IUserService _userService;
        public SheduleController(ISheduleService sheduleService, IUserService userService)
        {
            _sheduleService = sheduleService;
            _userService = userService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var sheduleDate = _sheduleService.GetSheduleForUser(_userService.CurrentUserId, DateTime.Now);
            return View(sheduleDate);
        }

        [HttpPost]
        [Authorize(Roles = "Староста, Администратор")]
        public async Task<IActionResult> LoadFile(IFormFile uploadedFile)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                uploadedFile.CopyTo(memoryStream);
                var result = _sheduleService.LoadSheduleFromFile(memoryStream);
                if (result.StatusCode == 200)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }


        [HttpPost]
        [Authorize(Roles = "Староста, Администратор")]
        public async Task<IActionResult> SynchronizeShedule(string date, int weekNumber)
        {
            var startSemesterDate = DateTime.Parse(date);
            var result = _sheduleService.SynchronizeSheduleAndSemester(startSemesterDate, weekNumber);
            if (result.StatusCode == 200)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        public IActionResult GetShedule(string sheduleDate)
        {
            var model = _sheduleService.GetSheduleForUser(_userService.CurrentUserId, DateTime.Parse(sheduleDate));
            return View("~/Views/Shedule/_CalendarSheduleTimeSpendings.cshtml", model.Lessons);
        }

        [Authorize(Roles = "Староста, Заместитель старосты")]
        public IActionResult GetAbsence(LessonUi lesson)
        {
            var model = _sheduleService.GetAbsence(lesson);
            return PartialView("~/Views/Shedule/_Absences.cshtml", model);
        }

        [Authorize(Roles = "Староста, Заместитель старосты")]
        [HttpPost]
        public async Task<IActionResult> SaveAbsence(AbsenceFormUi absenceForm, Dictionary<string, object> de)
        {
            var baseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            var result = await _sheduleService.SaveAbsence(absenceForm.IsPresent, 
                absenceForm.CalendarSheduleTimeSpendingId, baseUrl);
            if (result.StatusCode == 200)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Roles = "Студент, Староста, Заместитель старосты")]
        public IActionResult GetAbsencesOnPeriod()
        {
            return PartialView("~/Views/Shedule/_AbsencesDates.cshtml", _sheduleService.GetSemesterDate());
        }

        [Authorize(Roles = "Студент, Староста, Заместитель старосты")]
        [HttpPost]
        public IActionResult GetAbsencesOnPeriod(string startDate, string endDate, Dictionary<string, object> de)
        {
            var model = _sheduleService.GetAbsencesOnPeriod(DateTime.Parse(startDate), DateTime.Parse(endDate));
            return View("~/Views/Shedule/_CalendarSheduleTimeSpendings.cshtml", model.Lessons);
        }
    }
}