using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using WatchitWebApp.Entities;
using WatchitWebApp.Models;

namespace WatchitWebApp.Controllers
{
    public class SubscriberController : Controller
    {
        private static WatchitDBContext _dbContext = new WatchitDBContext(new DbContextOptions<WatchitDBContext>());
        private readonly SubscribeRepository _subscribeRepository = new SubscribeRepository(_dbContext);

        public SubscriberController()
        {

        }

        [HttpPost]
        public IActionResult Sub(int creator, int current)
        {
            Subscribe sub = _subscribeRepository.GetSub(current, creator);
            if (sub == null)
            {
                Subscribe newSub = new Subscribe() { UserIdCreator = creator, UserIdSubscriber = current, Created = DateTime.Now, NotificationStatus = Notification.GET };
                _subscribeRepository.Subscribe(newSub);
                return Json("Added");
            }
            else {
                _subscribeRepository.Unsubscribe(sub);
                return Json("Deleted");
            }
        }
    }
}
