using System;
using System.ComponentModel.DataAnnotations;

namespace ViralLinks.Data
{
    public class FileMetaData
    {
        [Key]
        public string Id { get; set ;}
        public string URI { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}