using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EJ.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text;
using EJ.Domain.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SHARED.Web.Caching;

namespace EJ.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceCache _serviceCache;
        private readonly IUserService _userService;

        private string sessionCloseKey => "SessionClosed_" + Request.Headers["MS-ASPNETCORE-TOKEN"].FirstOrDefault();

        public HomeController(IServiceCache serviceCache, IUserService userService)
        {
            _serviceCache = serviceCache;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
