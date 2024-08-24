using System.Collections;
using System.Linq;
using WatchitWebApp.Entities;

namespace WatchitWebApp.ViewModels
{
    public class ChannelViewModel
    {
        public UserInfo Creator { get; set; }
        public IEnumerable Videos { get; set; }
    }
}
