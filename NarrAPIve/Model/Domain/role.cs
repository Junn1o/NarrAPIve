using System.ComponentModel.DataAnnotations;

namespace NarrAPIve.Model.Domain
{
    public class role
    {
        [Key]
        public int role_id { get; set; }
        public string role_name { get; set; }
        public string role_description { get; set; }
        public List<credential> credential { get; set; }
    }
}
