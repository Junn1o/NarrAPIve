using System.ComponentModel.DataAnnotations;

namespace NarrAPIve.Model.Domain
{
    public class credential_user_temp
    {
        [Key]
        public int id { get; set; }
        public Guid user_id { get; set; }
        public Guid cred_id { get; set; }
    }
}
