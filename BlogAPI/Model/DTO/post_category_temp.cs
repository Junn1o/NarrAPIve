using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Model.DTO
{
    public class post_category_temp
    {
        [Key]
        public Guid id { get; set; }
        public Guid category_id { get; set; }
        public Guid post_id { get; set; }
        public category category { get; set; }
        public post post { get; set; }
    }
}
