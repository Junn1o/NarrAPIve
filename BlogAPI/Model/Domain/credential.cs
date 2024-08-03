using System.ComponentModel.DataAnnotations;
namespace BlogAPI.Model.Domain
{
    public class credential
    {
        [Key]
        public Guid cred_id { get; set; }
        public Guid user_id { get; set; }
        public int cred_roleid { get; set; }
        public string cred_userName { get; set; }
        public string cred_passWord { get; set; }
        public DateTime cred_createDate { get; set; }
        public user user { get; set; }
        public role role { get; set; }
    }
}
