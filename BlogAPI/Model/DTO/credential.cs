using System.ComponentModel.DataAnnotations;
namespace BlogAPI.Model.DTO
{
    public class credential
    {
        [Key]
        public Guid user_id { get; set; }
        public string cred_role { get; set; }
        public string cred_userName { get; set; }
        public string cred_passWord { get; set; }
        public string cred_createDate { get; set; }
        public user user { get; set; }
    }
}
