using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using WatchitWebApp.Entities;
using WatchitWebApp.Models;
using WebSite.Common;

namespace WebSite.Controllers
{
    public class AccountController : Controller
    {
        private static readonly WatchitDBContext _dBContext = new WatchitDBContext(new DbContextOptions<WatchitDBContext>());

        private readonly UserRepository _userRepository = new UserRepository(_dBContext);
        private readonly UserInfoRepository _userInfoRepository = new UserInfoRepository(_dBContext);

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckUser(string Email, string Password)
        {
            User user = _userRepository.GetUserByEmail(Email);
            if (user != null && SecurePasswordHasher.Verify(Password, user.Password))
            {


                var claims = new List<Claim>
                {
                    new Claim("Email", user.Email),
                    new Claim("Country", "UA"),
                    new Claim("Login", user.Login)
                };


                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Main", "Home");
            }
            else
            {
                return View("UserNotFound");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult RegisterNewUser(User user, string CPassword)
        {
            if (user.Password == CPassword)
            {
                if (_userRepository.AddUser(user))
                {
                    UserInfo userInfo = new UserInfo()
                    {
                        UserId = user.Id,
                        Avatar = "/img/avatars/avatar.png",
                        Info = "Інформацію не надано.",
                        Adress = "",
                        PhoneNumber = ""
                    };

                    if (_userInfoRepository.AddInfo(userInfo)) {
                        return View("UserCreated");
                    }
                }
            }
            return View("UserNotCreated");
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Profile() {
            var userEmail = User.FindFirstValue("Email");
            User user = _userRepository.GetUserByEmail(userEmail);
            UserInfo info = _userInfoRepository.GetUserInfoById(user.Id);
            info.User = user;

            return View(info);
        }
    }
}
