using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Model.Domain
{
    public class user
    {
        [Key]
        public Guid user_id { get; set; }
        public string user_firstName { get; set; }
        public string user_lastName { get; set; }
        public bool gender { get; set; }
        public DateTime user_birthday { get; set; }
        public List<post> post { get; set; }
        public credential credential { get; set; }
        public string? user_avatar {  get; set; }
    }
}
