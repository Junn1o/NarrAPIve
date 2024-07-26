using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Model.DTO
{
    public class post
    {
        [Key]
        public Guid post_id {  get; set; }
        public string post_title { get; set; }
        public string post_shortDes { get; set; }
        public string post_longDes { get; set;}
        public bool post_status { get; set; }
        public DateTime post_createDate { get; set; }
        public bool post_hidden { get; set; }
        public bool post_type { get; set; }
        public List<post_category_temp> post_category_temp { get; set; }
        public user user { get; set; }
    }
}
