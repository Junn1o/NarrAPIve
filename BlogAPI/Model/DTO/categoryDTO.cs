﻿namespace BlogAPI.Model.DTO
{
    public class CategoryDTO
    {
        public Guid categoryId { get; set; }
        public string categoryName { get; set; }
    }
    public class CategoryWithIdDTO
    {
        public List<PostListDTO> post { get; set; }
        public int totalResult { get; set; }
        public int totalPages { get; set; }
        public class PostListDTO
        {
            public Guid postId { get; set; }
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
    }
    public class CategoryRequestFromDTO
    {
        public string categoryName { get; set; }
    }
}
