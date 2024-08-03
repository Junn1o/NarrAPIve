using BlogAPI.Model.Domain;

namespace BlogAPI.Model.DTO
{
    public class userresultDTO
    {
        public List<userlistDTO> user { get; set; }
        public int totalResult { get; set; }
        public int totalPages { get; set; }
        public class userlistDTO
        {
            public Guid user_id { get; set; }
            public string user_firstName { get; set; }
            public string user_lastName { get; set; }
            public string user_birthDate { get; set; }
            public string user_avatar { get; set; }
        }
    }
    public class userwithidDTO
    {
        public string user_firstName { get; set; }
        public string user_lastName { get; set; }
        public string user_birthDate { get; set; }
        public string user_avatar { get; set; }
    }
    public class userrequestformDTO
    {
        public string user_firstName { get; set; }
        public string user_lastName { get; set; }
        public string user_birthDate { get; set; }
        //public string? user_avatar { get; set; }
        public string cred_userName { get; set; }
        public string cred_passWord { get; set; }
        public IFormFile? attach_file { set; get; }
    }
}
