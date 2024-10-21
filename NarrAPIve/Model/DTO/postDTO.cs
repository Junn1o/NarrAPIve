using BlogAPI.Model.Domain;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Model.DTO
{

    public class PostWithIdDTO
    {
        public Guid postId { get; set; }
        public Guid userId { get; set; }
        public string postTitle { get; set; }
        public string postDescription { get; set; }
        public string postStatus { get; set; }
        public string postCreateDate { get; set; }
        public string postHidden { get; set; }
        public string postType { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public int totalVolume { get; set; }
        public List<VolumeListDTO> volumeList { get; set; }
        public class VolumeListDTO
        {
            public Guid volumeId { get; set; }
            public string volumeTitle { get; set; }
            public string volumeCreateDate { get; set; }
        }
        public List<CategoryListDTO> categorylist { get; set; }
        public class CategoryListDTO
        {
            public Guid categoryId { get; set; }
            public string categoryName { get; set; }
        }
    }
    public class PostWithUserIdDTO
    {
        public Guid postId { get; set; }
        public Guid userId { get; set; }
        public string postTitle { get; set; }
        public string postDescription { get; set; }
        public string postStatus { get; set; }
        public string postCreateDate { get; set; }
        public string postHidden { get; set; }
        public string postType { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public int totalVolume { get; set; }
        public List<VolumeListDTO> volumeList { get; set; }
        public class VolumeListDTO
        {
            public Guid volumeId { get; set; }
            public string volumeTitle { get; set; }
            public string volumeCreateDate { get; set; }
        }
        public List<CategoryListDTO> categorylist { get; set; }
        public class CategoryListDTO
        {
            public Guid categoryId { get; set; }
            public string categoryName { get; set; }
        }
    }
    public class VolumeDetailWithIdDTO
    {
        public Guid volumeId { get; set; }
        public string volumeTitle { get; set; }
        public string volumeCreateDate { get; set; }
        public List<ChapterDTO> chapterList { get; set; }
        public class ChapterDTO
        {
            public Guid chapterId { get; set; }
            public string chapterTitle { get; set; }
            public string chapterContent { get; set; }
        }
    }
    public class PostResultDTO
    {
        public class PostListDTO
        {
            public Guid postId { get; set; }
            public Guid userId { get; set; }
            public string postTitle { get; set; }
            public string postDescription { get; set; }
            public string postStatus { get; set; }
            public DateTime postCreateDate { get; set; }
            public string postHidden { get; set; }
            public string postType { get; set; }
            public int totalVolume { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public List<string> volumeList { get; set; }
            public List<string> categoryName { get; set; }
        }
        public List<PostListDTO> post { get; set; }
        public int totalResult { get; set; }
        public int totalPages { get; set; }
    }
    public class PostRequestFormDTO
    {
        [Required(ErrorMessage = "The {0} filed is required.")]
        public string postTitle { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        public string postDescription { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        public bool postStatus { get; set; }
        public bool postHidden { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        public bool postType { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        public string postImage { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        public Guid user_id { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        public List<Guid> category_ids { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        [DataType(DataType.Upload)]
        public IFormFile? attachFile { get; set; }
    }
    public class VolumeRequestFormDTO
    {
        [Required(ErrorMessage = "The {0} filed is required.")]
        public Guid postId { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        public string volumeTitle { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        public string volumeDescription { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        public string volumeImage { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        public IFormFile? attachFile { get; set; }
    }
    public class ChapterRequestFormDTO
    {
        [Required(ErrorMessage = "The {0} filed is required.")]
        public Guid volumeId { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        public string chapterTitle { get; set; }
        [Required(ErrorMessage = "The {0} filed is required.")]
        public string chapterContent { get; set; }
    }
    public class ChapterImageRequestFormDTO
    {
        public Guid volumeId { get; set; }
        public Guid postId { get; set; }
        public string chapterTitle { get; set; }
        public string chapterContentImage { get; set; }
        public IFormFile? attachFile { get; set; }
    }
}
