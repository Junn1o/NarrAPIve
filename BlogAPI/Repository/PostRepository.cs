using BlogAPI.Data;
using BlogAPI.Model.Domain;
using BlogAPI.Model.DTO;
using BlogAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext appDbContext;
        public PostRepository(AppDbContext _appDbContext)
        {
            this.appDbContext = _appDbContext;
        }
        public PostResultDTO getfullpost(int pageNumber = 1, int pageSize = 10)
        {
            var postQuery = appDbContext.post.Select(p => new PostResultDTO.PostListDTO()
            {
                postId = p.post_id,
                postTitle = p.post_title,
            }).AsSplitQuery();
            var skipResults = (pageNumber - 1) * pageSize;
            if (postQuery == null)
            {
                return null;
            }
            else
            {
                var postList = postQuery.Skip(skipResults).Take(pageSize).ToList();
                var totalResult = postQuery.Count();
                var totalPage = (int)Math.Ceiling((double)totalResult / pageSize);
                var result = new PostResultDTO
                {
                    post = postList.ToList(),
                    totalResult = totalResult,
                    totalPages = totalPage,
                    
                };
                return result;
            }
        }
        public PostrequestFormDTO addpostDTO(PostrequestFormDTO addpost)
        {
            //var user = _appDbContext.User.FirstOrDefault(a => a.Id == addpost.userId);
            var userid = appDbContext.user.FirstOrDefault(u => u.user_id == addpost.user_id);
            var postDomain = new post
            {
                post_title = addpost.postTitle,
                post_description = addpost.postDescription,
                post_hidden = addpost.postHidden,
                post_status = addpost.postStatus,
                post_type = addpost.postType,
                post_createDate = (DateTime.Now),
            };
            appDbContext.post.Add(postDomain);
            appDbContext.SaveChanges();
            return addpost;
        }
    }
}
