using System;
using System.ComponentModel.DataAnnotations;

namespace ViralLinks.Data
{
    public class ProfilePicture
    {
        [Key]
        public string UserID { get; set; }
        public string Extension { get; set; }
        public DateTime LastUpdate { get; set; }
    }

    public class Media
    {
        [Key]
        public Guid ID { get; set;}
        public string UserID { get; set; }
        public string BlobUrl { get; set; }
        public string MediaType { get; set; }
    }

    public class MediaThumbnail
    {
        public Guid ID { get; set; }
        public string MediaID { get; set; }
        public string BlobUrl { get; set; }
    }

    public enum MediaType
    {
        image,
        video
    }
}