using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using EJ.Domain.Services;
using EJ.Domain.Services.AuthorizationServices;
using EJ.Entities.Models;
using EJ.Models.Enums;
using EJ.Models.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using EJ.Web.Models;
using SHARED.Common.Extensions;
using SHARED.Web.Caching;

namespace EJ.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IServiceCache _serviceCache;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IEMailService _eMailService;
        private string sessionCloseKey => "SessionClosed_" + Request.Headers["MS-ASPNETCORE-TOKEN"].FirstOrDefault();

        public AccountController(IUserService userService, IServiceCache serviceCache,
        IAuthorizationService authorizationService, IEMailService eMailService,
        IGroupService groupService)
        {
            _userService = userService;
            _serviceCache = serviceCache;
            _authorizationService = authorizationService;
            _eMailService = eMailService;
            _groupService = groupService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Groups = _groupService.GetGroups();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.Exists(model.Email))
                {
                    var user = await _userService.Register(model);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, code = user.Code },
                        protocol: HttpContext.Request.Scheme);

                    await _eMailService.SendAsync(user.Email, $"Подтвердите электронный адрес",
                        $"<h3>Web-приложение \"Электронный журнал\"</h3><br/>" +
                        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>ссылка</a><br/>" +
                        $"<h4>Автор - Чаевский Андрей</h4>");
                    return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
                }
                else
                {
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            foreach (var errorValue in ModelState.Keys)
            {
                if (ModelState[errorValue].Errors.Count != 0)
                {
                    foreach (var error in ModelState[errorValue].Errors.ToList())
                    {
                        ModelState.AddModelError(errorValue, error.ErrorMessage);
                    }
                }
            }
            ViewBag.Groups = _groupService.GetGroups();
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            _userService.ClearCurrentUserCache();
            if (ModelState.IsValid)
            {
                UserInfoViewModel userInfoUi = await _userService.Login(model);
                if (userInfoUi.ErrorMessage == null)
                {
                    await Authenticate(userInfoUi); // аутентификация

                    return RedirectToAction("Index", "Shedule");
                }
                ModelState.AddModelError("", userInfoUi.ErrorMessage);
            }
            return View(model);
        }
        private async Task Authenticate(UserInfoViewModel userInfoUi)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, userInfoUi.Role.GetDescription()),
                new Claim(ClaimTypes.Email, userInfoUi.Email)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            var q = _userService.CurrentUserLogin;
            HttpContext.Session.Set("PreviousLogin", Encoding.UTF8.GetBytes(_userService.CurrentUserLogin));
            _serviceCache.AddOrReplace(sessionCloseKey, false);
            _userService.ClearCurrentUserCache();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ConfirmEmail(int userId, string code)
        {
            var result = _authorizationService.ConfirmEmailAsync(userId, code);
            if (result.Result.Succeeded)
                return RedirectToAction("Login", "Account");
            else
                return View("Error");
        }
    }
}
