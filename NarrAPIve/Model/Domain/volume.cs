using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace NarrAPIve.Model.Domain
{
    public class volume
    {
        [Key]
        public Guid volume_id { get; set; }
        public Guid post_id { get; set; }
        public string volume_title { get; set; }
        public DateTime volume_createDate { get; set; }
        public string volume_description { get; set; }
        public string? volume_image  { get; set; }
        public post post { get; set; }
        public List<chapter> chapter { get; set; }
    }
}
