using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Model.DTO
{
    public class user
    {
        [Key]
        public Guid user_id { get; set; }
        public string user_firstName { get; set; }
        public string user_lastName { get; set; }
        public DateTime user_birthDate { get; set; }
        public List<post> post { get; set; }
        public credential credential { get; set; }
    }
}
