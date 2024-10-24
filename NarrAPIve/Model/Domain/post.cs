using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NarrAPIve.Model.Domain
{
    public class post
    {
        [Key]
        public Guid post_id { get; set; }
        public Guid user_id { get; set; }
        public string? post_image { get; set; }
        public string post_title { get; set; }
        public string? post_description { get; set; } 
        public bool post_status { get; set; } // approve or not?
        public DateTime post_createDate { get; set; }
        public bool post_hidden { get; set; }
        public List<post_category_temp> post_category_temp { get; set; }
        public user user { get; set; } 
        public List<volume>? volume { get; set; }
    }
}
