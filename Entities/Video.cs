using System;
using System.ComponentModel.DataAnnotations;
using WebSite.Entities;

namespace WatchitWebApp.Entities
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public string PreviewSrc { get; set; }
        public string VideoSrc { get; set; }
        public string FileName { get; set; }
        public string VideoType { get; set; } = string.Empty;
        public DateTime DateCreation { get; set; } = DateTime.Now;
        public DateTime DatePublished { get; set; }
        public DateTime DateUpdated { get; set; }
        public Status Status { get; set; } = Status.CREATED;
        public string NanoId { get; set; } = NanoIdGenerator.Generate();
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
