namespace BlogAPI.Model.DTO
{
    public class CredentialDTO
    {
        public string userName { get; set; }
        public string password { get; set; }
        //public guid? user_id { get; set; }
        //public string? firstname { get; set; }
        //public string? lastname { get; set; }
        //public datetime? birthdate { get; set; }
        //public string? avatar { get; set; }
        //public string? rolename { get; set; }
    }
    public class LoginDataDTO
    {
        public Guid user_id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthDate { get; set; }
        public string? avatar { get; set; }
        public string roleName { get; set; }
    }

}
