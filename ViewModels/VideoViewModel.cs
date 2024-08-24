using System.Collections.Generic;
using System.Linq;
using WatchitWebApp.Entities;

namespace WatchitWebApp.ViewModels
{
    public class VideoViewModel
    {
        public Video Video { get; set; }
        public Subscribe Subscribe { get; set; }
        public int Subscribes { get; set; }
        public UserInfo CurrentUser { get; set; }
        public UserInfo Creator { get; set; }
        public IEnumerable<Video> VideosList { get; set; }
        // subs
    }
}
