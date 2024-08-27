namespace BlogAPI.Model.DTO
{
    public class CredentialDTO
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
    public class LoginDataDTO
    {
        public Guid user_id { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthDate { get; set; }
        public string? avatar { get; set; } 
        public string roleName { get; set; }
    }

}
