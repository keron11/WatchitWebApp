using System.Linq;
using WatchitWebApp.Entities;

namespace WatchitWebApp.Models
{
    public class SubscribeRepository
    {
        private readonly WatchitDBContext _watchitDBContext;

        public SubscribeRepository(WatchitDBContext _watchitDBContext)
        {
            this._watchitDBContext = _watchitDBContext;
        }
        public int GetCountSubsById(int userId) => _watchitDBContext.Subscribes.Count(s => s.UserIdCreator == userId);
        public Subscribe GetSub(int userId, int? creatorId) => _watchitDBContext.Subscribes.FirstOrDefault(s => s.UserIdSubscriber == userId && s.UserIdCreator == creatorId);
        internal bool Subscribe(Subscribe sub)
        {
            _watchitDBContext.Subscribes.Add(sub);
            return _watchitDBContext.SaveChanges() == 1 ? true : false;
        }
        internal bool Unsubscribe(Subscribe sub)
        {
            _watchitDBContext.Subscribes.Remove(sub);
            return _watchitDBContext.SaveChanges() == 1 ? true : false;
        }
    }
}
