using BlogAPI.Model.Domain;

namespace BlogAPI.Model.DTO
{

    public class PostwithIdDTO
    {
        public string postTitle { get; set; }
        //public string post_shortDes { get; set; }
        public string postDescriptiop { get; set; }
        public bool postStatus { get; set; }
        public DateTime postCreateDate { get; set; }
        public bool postHidden { get; set; }
        public bool postType { get; set; }
        public string totalVolume { get; set; }
        public string chapterCount { get; set; }
        //public List<string> chapter_list { get; set; }
        public List<string> volumeList { get; set; }
    }
    public class PostVolumeWithIdDTO
    {
        public string volumeTitle { get; set; }
        public List<string> chapterList { get; set; }
        public List<string> chapterContent { get; set; }
        public string volumeCreatDate { get; set; }
    }
    public class PostResultDTO
    {
        public class PostListDTO
        {
            public Guid postId { get; set; }
            public string postTitle { get; set; }
            public string postShortDes { get; set; }
            //public string postLongDes { get; set; }
            public bool postStatus { get; set; }
            public string postCreateDate { get; set; }
            public bool postHidden { get; set; }
            public bool postType { get; set; }
            public Guid volumeId { get; set; }
        }
        public List<PostListDTO> post { get; set; }
        public int totalResult { get; set; }
        public int totalPages { get; set; }
    }
    public class PostrequestFormDTO
    {
        public string postTitle { get; set; }
        //public string post_shortDes { get; set; }
        public string postDescription { get; set; }
        public bool postStatus { get; set; }
        //public DateTime? postCreateDate { get; set; }
        public bool postHidden { get; set; }
        public bool postType { get; set; }
        public Guid user_id { get; set; }
        public List<Guid> category_ids { get; set; }
    }
    public class VolumeRequestFormDTO
    {
        public string volumeTitle { get; set; }
    }
    public class ChapterRequestFormDTO
    {
        public Guid volumeId { get; set; }
        public string chapterTitle { get; set; }
        public string chapterContent { get; set; }
    }
}
