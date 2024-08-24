using System.Linq;
using WatchitWebApp.Entities;
using WebSite.Common;
using WebSite.Entities;

namespace WatchitWebApp.Models
{
    public class VideoRepository
    {
        private readonly WatchitDBContext _watchitDBContext;

        public VideoRepository(WatchitDBContext _watchitDBContext)
        {
            this._watchitDBContext = _watchitDBContext;
        }

        public Video GetVideoByNId(string nanoId) => _watchitDBContext.Videos.FirstOrDefault(v => v.NanoId == nanoId);
        
        public IQueryable<Video> GetAllUserVideo(int userId) => _watchitDBContext.Videos.Where(v => v.UserId == userId);
        public IQueryable<Video> GetAllUserPublishedVideo(int userId) => _watchitDBContext.Videos.Where(v => v.UserId == userId && v.Status == Status.PUBLISHED);
        public IQueryable<Video> GetAllPublishedVideo() => _watchitDBContext.Videos.Where(v => v.Status == Status.PUBLISHED);

        internal bool AddVideo(Video video)
        {
            _watchitDBContext.Videos.Add(video);
            return _watchitDBContext.SaveChanges() == 1 ? true : false;
        }

        public bool DeleteVideoByNId(string nanoId) {
            _watchitDBContext.Remove(_watchitDBContext.Videos.First(v => v.NanoId == nanoId));
            return _watchitDBContext.SaveChanges() == 1 ? true : false;
        }
    }
}
