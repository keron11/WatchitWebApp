using System.Linq;
using WatchitWebApp.Entities;
using WebSite.Common;

namespace WatchitWebApp.Models
{
    public class UserInfoRepository
    {
        private readonly WatchitDBContext _watchitDBContext;

        public UserInfoRepository(WatchitDBContext _watchitDBContext)
        {
            this._watchitDBContext = _watchitDBContext;
        }

        public UserInfo GetUserInfoById(int id) => _watchitDBContext.UserInfo.SingleOrDefault(ui => ui.UserId == id);
        internal bool AddInfo(UserInfo uInfo)
        {
            _watchitDBContext.UserInfo.Add(uInfo);
            return _watchitDBContext.SaveChanges() == 1 ? true : false;
        }
    }
}
