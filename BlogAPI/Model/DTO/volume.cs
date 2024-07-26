using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Model.DTO
{
    public class volume
    {
        [Key]
        public Guid volume_id { get; set; }
        public Guid post_id { get; set; }
        public string volume_name { get; set; }
        public post post { get; set; }
        public List<chapter> chapter { get; set; }
    }
}
