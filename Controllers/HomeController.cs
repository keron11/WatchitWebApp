using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WatchitWebApp.Entities;
using WatchitWebApp.Models;

namespace WatchitWebApp.Controllers
{
    public class HomeController : Controller
    {
        private static WatchitDBContext _dbContext = new WatchitDBContext(new DbContextOptions<WatchitDBContext>());
        private readonly UserRepository _userRepository;
        private readonly UserInfoRepository _userInfoRepository;
        private readonly VideoRepository _videoRepository;

        public HomeController()
        {
            _userRepository = new UserRepository(_dbContext);
            _userInfoRepository = new UserInfoRepository(_dbContext);
            _videoRepository = new VideoRepository(_dbContext);
        }

        public IActionResult Index()
        {
            var userEmail = User.FindFirstValue("Email");

            if (userEmail != null) {
                return RedirectToAction("Main");
            } else return View();
        }

        public IActionResult Main()
        {
            var userEmail = User.FindFirstValue("Email");
            User user = _userRepository.GetUserByEmail(userEmail);
            UserInfo info = _userInfoRepository.GetUserInfoById(user.Id);
            info.User = user;

            IEnumerable<Video> videos = _videoRepository.GetAllPublishedVideo().ToList();

            foreach (Video video in videos) {
                User us = _userRepository.GetUserById(video.UserId);
                video.User = us;
            }

            return View(videos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
