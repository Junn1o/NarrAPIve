using System.ComponentModel.DataAnnotations;

namespace NarrAPIve.Model.Domain
{
    public class category
    {
        [Key]
        public Guid category_id { get; set; }
        public string category_name { get; set; }
        public List<post_category_temp> post_category_temp { get; set; }
    }
}
