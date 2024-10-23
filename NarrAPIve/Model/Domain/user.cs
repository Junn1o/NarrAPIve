using System.ComponentModel.DataAnnotations;

namespace NarrAPIve.Model.Domain
{
    public class user
    {
        [Key]
        public Guid user_id { get; set; }
        public string user_firstName { get; set; }
        public string user_lastName { get; set; }
        public bool? user_gender { get; set; }
        public DateTime user_birthdate { get; set; }
        public List<post> post { get; set; }
        public credential credential { get; set; }
        public string? user_avatar {  get; set; }
    }
}
