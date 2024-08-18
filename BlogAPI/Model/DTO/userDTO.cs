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
            public Guid userId { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string birthdate { get; set; }
            public string avatar { get; set; }
            public string gender { get; set; }
        }
    }
    public class UserWithIdDTO
    {
        public Guid userId { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string birthdate { get; set; }
        public string avatar { get; set; }
        public string gender { get; set; }
        public string roleName { get; set; }
        public string dateCreate { get; set; }
    }
    public class UserRequestFormDTO
    {
        public Guid userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool gender { get; set; }
        public string birthdate { get; set; }
        public string? avatar { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public IFormFile? attachFile { set; get; }
    }
    public class LoginDTO
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
    public class ResponseDataDTO
    {
        public Guid userId { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthdate { get; set; }
        public string? avatar { get; set; }
        public string roleName { get; set; }
    }
}
