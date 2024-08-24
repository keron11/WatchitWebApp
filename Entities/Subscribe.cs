using System;

namespace WatchitWebApp.Entities
{
    public class Subscribe
    {
        public int Id { get; set; }
        public int UserIdSubscriber { get; set; }
        public int UserIdCreator { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public Notification NotificationStatus { get; set; } = Notification.GET;
    }
}
