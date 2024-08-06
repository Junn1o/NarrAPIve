using BlogAPI.Model.Domain;

namespace BlogAPI.Model.DTO
{
    public class UserResultDTO
    {
        public List<UserListDTO> user { get; set; }
        public int totalResult { get; set; }
        public int totalPages { get; set; }
        public class UserListDTO
        {
            public Guid user_id { get; set; }
            public string user_firstName { get; set; }
            public string user_lastName { get; set; }
            public string user_birthDate { get; set; }
            public string user_avatar { get; set; }
        }
    }
    public class UserWithIdDTO
    {
        public string user_firstName { get; set; }
        public string user_lastName { get; set; }
        public string user_birthDate { get; set; }
        public string user_avatar { get; set; }
    }
    public class UserRequestFormDTO
    {
        public string user_firstName { get; set; }
        public string user_lastName { get; set; }
        public string user_birthDate { get; set; }
        //public string? user_avatar { get; set; }
        public string cred_userName { get; set; }
        public string cred_password { get; set; }
        public IFormFile? attach_file { set; get; }
    }
    //public class LoginDataDTO
    //{
    //    public Guid user_id { get; set; }
    //    public string firstName { get; set; }
    //    public string lastName { get; set; }
    //    public DateTime birthDate { get; set; }
    //    public string? avatar { get; set; }
    //    public string roleName { get; set; }
    //}
}
