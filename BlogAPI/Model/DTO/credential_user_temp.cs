using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Model.DTO
{
    public class credential_user_temp
    {
        [Key]
        public int id { get; set; }
        public Guid user_id { get; set; }
        public Guid cred_id { get; set; }
    }
}
