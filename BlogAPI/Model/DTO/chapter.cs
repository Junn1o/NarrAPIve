using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Model.DTO
{
    public class chapter
    {
        [Key]
        public Guid chapter_id { get; set; }
        public Guid volume_id { get; set; }
        public Guid post_id { get; set; }
        public string chapter_content { get; set; }
        public volume volume { get; set; }
        public post post { get; set; }
    }
}
