using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WatchitWebApp.Entities;
using WatchitWebApp.Models;
using WatchitWebApp.ViewModels;

namespace WatchitWebApp.Controllers
{
    public class ChannelController : Controller
    {
        private static readonly WatchitDBContext _dBContext = new WatchitDBContext(new DbContextOptions<WatchitDBContext>());
        private readonly VideoRepository _videoRepository = new VideoRepository(_dBContext);
        private readonly UserRepository _userRepository = new UserRepository(_dBContext);
        private readonly UserInfoRepository _userInfoRepository = new UserInfoRepository(_dBContext);
        private readonly SubscribeRepository _subscribeRepository = new SubscribeRepository(_dBContext);

        public IActionResult Main() {
                string userId = Request.Query["u"];
                ChannelViewModel channelViewModel = new ChannelViewModel();

                User user = _userRepository.GetUserByLogin(userId);
                UserInfo userInfo = _userInfoRepository.GetUserInfoById(user.Id);

                userInfo.User = user;
                IEnumerable<Video> videos = _videoRepository.GetAllUserPublishedVideo(user.Id).ToList();

                channelViewModel.Creator = userInfo;
                channelViewModel.Videos = videos;

                return View(channelViewModel);
        }
    }
}
