using BlogAPI.Model.Domain;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "The {0} filed is required.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The {0} field cannot be empty or whitespace.")]
        public string firstName { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The {0} field cannot be empty or whitespace.")]
        public string lastName { get; set; }
        public bool gender { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        [Range(typeof(DateTime), "1/1/1900", "12/31/2023", ErrorMessage = "Birthdate must be between 01/01/1900 and 12/31/2023.")]
        public string birthdate { get; set; }
        public string? userAvatar { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        public string userName { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? attachFile { set; get; }
    }

    public class LoginDTO
    {
        [Required(ErrorMessage = "The {0} filed is required.")]
        public string userName { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        [DataType(DataType.Password)]
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
