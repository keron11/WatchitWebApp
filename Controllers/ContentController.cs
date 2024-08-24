using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WatchitWebApp.Entities;
using WatchitWebApp.Models;
using WatchitWebApp.ViewModels;
using WebSite.Entities;

namespace WatchitWebApp.Controllers
{
    public class ContentController : Controller
    {
        private static readonly WatchitDBContext _dBContext = new WatchitDBContext(new DbContextOptions<WatchitDBContext>());
        private readonly VideoRepository _videoRepository = new VideoRepository(_dBContext);
        private readonly UserRepository _userRepository = new UserRepository(_dBContext);
        private readonly UserInfoRepository _userInfoRepository = new UserInfoRepository(_dBContext);
        private readonly SubscribeRepository _subscribeRepository = new SubscribeRepository(_dBContext);
        private readonly IWebHostEnvironment _webHost;

        public ContentController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatingVideo() {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult VideoSettings(string nanoId) {
            Video video = _videoRepository.GetVideoByNId(nanoId);
            return View(video);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditVideo(IFormFile filePic, Video video) {
 
            return View("AllVideos"); 
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadVideo(IFormFile file, IFormFile filePic, Video video) {
            if (Path.GetExtension(file.FileName) == ".mp4") {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "videos");
                string previewsFolder = Path.Combine(_webHost.WebRootPath, "videos\\previews");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                if (!Directory.Exists(previewsFolder))
                {
                    Directory.CreateDirectory(previewsFolder);
                }

                string correctName = DateTime.Now.Day
                    + "_" +
                    DateTime.Now.Month
                    + "_" +
                    DateTime.Now.Year
                    + "_" + DateTime.Now.Second
                    + new Random().Next(DateTime.Now.Day, DateTime.Now.Year);

                string correctVideoName = correctName + Path.GetExtension(file.FileName);
                string correctPicName = "def_preview.png";

                string fileSavePath = Path.Combine(uploadsFolder, correctVideoName);
                
                if (filePic != null)
                {
                    correctPicName = correctName + Path.GetExtension(filePic.FileName);
                    string fileSavePicPath = Path.Combine(previewsFolder, correctPicName);
                    using (FileStream stream = new FileStream(fileSavePicPath, FileMode.Create))
                    {
                        await filePic.CopyToAsync(stream);
                    }
                }

                video.VideoSrc = "/videos/" + correctVideoName;
                video.PreviewSrc = "/videos/previews/" + correctPicName;

                if (video.Status == Status.PUBLISHED) {
                    video.DatePublished = DateTime.Now;
                }

                video.FileName = correctVideoName;
                video.VideoType = Path.GetExtension(file.FileName);

                var userEmail = User.FindFirstValue("Email");
                User user = _userRepository.GetUserByEmail(userEmail);
                video.UserId = user.Id;

                using (FileStream stream = new FileStream(fileSavePath, FileMode.Create)) {
                    await file.CopyToAsync(stream);
                }

                if (_videoRepository.AddVideo(video))
                {
                    ViewBag.TextColor = "success";
                    ViewBag.Message = file.FileName + " завантажено!";
                }
                else
                {
                    ViewBag.TextColor = "danger";
                    ViewBag.Message = "Сталася помилка, спробуйте ще раз";
                }
            return View("CreatingVideo");
        }
            ViewBag.TextColor = "danger";
            ViewBag.Message = "Сталася помилка , некоректний формат відео , спробуйте ще раз";
        return View("CreatingVideo");
    }

        [HttpPost]
        [Authorize]
        public IActionResult DeleteVideo(string nanoId)
        {
            var video = _videoRepository.GetVideoByNId(nanoId);

            var fileDeletePath = Path.Combine(_webHost.WebRootPath + "\\videos\\", video.FileName);

            if (System.IO.File.Exists(fileDeletePath) && _videoRepository.DeleteVideoByNId(nanoId))
            {
                System.IO.File.Delete(fileDeletePath);
                return RedirectToAction("AllVideos");
            }
            else return RedirectToAction("AllVideos");
        }
        [Authorize]
        public IActionResult AllVideos() {
            var userEmail = User.FindFirstValue("Email");
            User user = _userRepository.GetUserByEmail(userEmail);
            return View("AllVideos", _videoRepository.GetAllUserVideo(user.Id));
        }

        [Authorize]
        public IActionResult Watch() {
            string nId = Request.Query["v"];
            Video video = _videoRepository.GetVideoByNId(nId);

            if (video == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }
            else
            {
                VideoViewModel videoViewModel = new VideoViewModel();

                var userEmail = User.FindFirstValue("Email");
                User user = _userRepository.GetUserByEmail(userEmail);
                UserInfo currentUser = _userInfoRepository.GetUserInfoById(user.Id);

                UserInfo creator = _userInfoRepository.GetUserInfoById(video.UserId);
                creator.User = _userRepository.GetUserById(video.UserId);

                int subs = _subscribeRepository.GetCountSubsById(creator.User.Id);

                Subscribe sub = _subscribeRepository.GetSub(user.Id, video.UserId);

                videoViewModel.Video = video;
                videoViewModel.Subscribe = sub;
                videoViewModel.Subscribes = subs;
                videoViewModel.CurrentUser = currentUser;
                videoViewModel.Creator = creator;

                return View(videoViewModel);
            }
        }
    }
}
