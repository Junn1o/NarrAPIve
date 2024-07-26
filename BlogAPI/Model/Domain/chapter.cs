using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Model.Domain
{
    public class chapter
    {
        [Key]
        public Guid chapter_id { get; set; }
        public Guid volume_id { get; set; }
        public string chapter_content { get; set; }
        public volume volume { get; set; }
    }
}
