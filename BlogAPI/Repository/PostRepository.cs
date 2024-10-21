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
        private readonly Function function;
        public PostRepository(AppDbContext _appDbContext, Function _function)
        {
            this.appDbContext = _appDbContext;
            this.function = _function;
        }
        public PostResultDTO GetFullPost(int pageNumber = 1, int pageSize = 10)
        {
            var postQuery = appDbContext.post
                .Include(u=>u.user)
                .Include(p => p.post_category_temp).ThenInclude(c=>c.category)
                .Select(p => new PostResultDTO.PostListDTO()
            {
                postId = p.post_id,
                userId = p.user_id,
                postTitle = p.post_title,
                postCreateDate = p.post_createDate,
                postHidden = p.post_hidden == null ? "Hidden" : (p.post_hidden == true ? "Public" : "Private"),
                postStatus = p.post_hidden == true ? "Approved" : "Not approve",
                postType = p.post_type == true ? "Blog" : "Novel",
                postDescription = p.post_description,
                firstName = p.user.user_firstName,
                lastName = p.user.user_lastName,
                categoryName = p.post_category_temp.Select(cn=>cn.category.category_name).ToList(),
                totalVolume = p.volume.Count(),
                volumeList = p.volume.Select(v=>v.volume_title).ToList(),
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
        public PostWithIdDTO PostWithId(Guid postId)
        {
            var postDomain = appDbContext.post
                .Include(u => u.user)
                .Include(p => p.post_category_temp).ThenInclude(c => c.category)
                .Where(u=>u.post_id == postId);
            var getPost = postDomain.Select(p => new PostWithIdDTO()
            {
                postId = p.post_id,
                userId = p.user_id,
                postCreateDate = p.post_createDate.ToString("dd/MM/yyyy"),
                postTitle = p.post_title,
                postDescription = p.post_description,
                postHidden = p.post_hidden == null ? "Hidden" : (p.post_hidden == true ? "Public" : "Private"),
                postStatus = p.post_hidden == true ? "Approved" : "Not approve",
                postType = p.post_type == true ? "Blog" : "Light Novel",
                totalVolume = p.volume.Count(),
                firstName = p.user.user_firstName,
                lastName = p.user.user_lastName,
                categorylist = p.post_category_temp.Select(c=> new PostWithIdDTO.CategoryListDTO()
                {
                   categoryId = c.category_id,
                   categoryName = c.category.category_name,
                }).ToList(),
                volumeList = p.volume.Select(v => new PostWithIdDTO.VolumeListDTO()
                {
                    volumeId = v.volume_id,
                    volumeTitle = v.volume_title,
                    volumeCreateDate = v.volume_createDate.ToString("dd/MM/yyyy")
                }).ToList(),
            }).FirstOrDefault();
            if (getPost == null)
                return null;
            return getPost;
        }
        public PostWithUserIdDTO PostWithUserId(Guid userId)
        {
            var postDomain = appDbContext.post
                .Include(u => u.user)
                .Include(p => p.post_category_temp).ThenInclude(c => c.category)
                .Where(u => u.user_id == userId);
            var getPost = postDomain.Select(p => new PostWithUserIdDTO()
            {
                postId = p.post_id,
                userId = p.user_id,
                postCreateDate = p.post_createDate.ToString("dd/MM/yyyy"),
                postTitle = p.post_title,
                postDescription = p.post_description,
                postHidden = p.post_hidden == null ? "Hidden" : (p.post_hidden == true ? "Public" : "Private"),
                postStatus = p.post_hidden == true ? "Approved" : "Not approve",
                postType = p.post_type == true ? "Blog" : "Light Novel",
                totalVolume = p.volume.Count(),
                firstName = p.user.user_firstName,
                lastName = p.user.user_lastName,
                categorylist = p.post_category_temp.Select(c => new PostWithUserIdDTO.CategoryListDTO()
                {
                    categoryId = c.category_id,
                    categoryName = c.category.category_name,
                }).ToList(),
                volumeList = p.volume.Select(v => new PostWithUserIdDTO.VolumeListDTO()
                {
                    volumeId = v.volume_id,
                    volumeTitle = v.volume_title,
                    volumeCreateDate = v.volume_createDate.ToString("dd/MM/yyyy")
                }).ToList(),
            }).FirstOrDefault();
            if (getPost == null)
                return null;
            return getPost;
        }
        public VolumeDetailWithIdDTO VolumeDetailWithId(Guid volumeId)
        {
            var volumeDomain = appDbContext.volume.Include(c=>c.chapter).Where(v => v.volume_id == volumeId);
            var getVolume = volumeDomain.Select(v => new VolumeDetailWithIdDTO()
            {
                volumeId = v.volume_id,
                volumeCreateDate = v.volume_createDate.ToString("dd/MM/yyyy"),
                volumeTitle = v.volume_title,
                chapterList = v.chapter.Select(c=> new VolumeDetailWithIdDTO.ChapterDTO()
                {
                    chapterId = c.chapter_id,
                    chapterTitle = c.chapter_title,
                    chapterContent = c.chapter_content,
                }).ToList(),
            }).FirstOrDefault();
            return getVolume;
        }
        public PostRequestFormDTO AddPost(PostRequestFormDTO addpostDTO)
        {
            //var userid = appDbContext.user.FirstOrDefault(u => u.user_id == addpost.user_id);
            var postDomain = new post
            {
                post_title = addpostDTO.postTitle,
                user_id = addpostDTO.user_id,
                post_description = addpostDTO.postDescription,
                post_hidden = addpostDTO.postHidden,
                post_status = addpostDTO.postStatus = false,
                post_type = addpostDTO.postType,
                post_createDate = (DateTime.Now),
            };
            appDbContext.post.Add(postDomain);
            appDbContext.SaveChanges();
            if(addpostDTO.attachFile != null)
                postDomain.post_image = addpostDTO.postImage = function.UploadImage(addpostDTO.attachFile, null, postDomain.post_id, null, null);
            foreach (var categoryid in addpostDTO.category_ids)
            {
                var post_category = new post_category_temp()
                {
                    post_id = postDomain.post_id,
                    category_id = categoryid,
                };
                appDbContext.post_category_temp.Add(post_category);
                appDbContext.SaveChanges();
            }
            appDbContext.SaveChanges();
            return addpostDTO;
        }
        public PostRequestFormDTO UpdatePost(Guid postId, PostRequestFormDTO updatepostDTO)
        {
            var postDomain = appDbContext.post
                .Include(p=>p.post_category_temp)
                .Where(pi=>pi.post_id == postId)
                .FirstOrDefault();
            if (postDomain == null)
                return null;
            postDomain.post_title = updatepostDTO.postTitle;
            postDomain.post_description = updatepostDTO.postDescription;
            postDomain.post_hidden = updatepostDTO.postHidden;
            postDomain.post_status = updatepostDTO.postStatus;
            postDomain.post_type = updatepostDTO.postType;
            if (updatepostDTO.attachFile != null)
                postDomain.post_image = updatepostDTO.postImage;
            if (postDomain.post_category_temp != null)
            {
                appDbContext.post_category_temp.RemoveRange(postDomain.post_category_temp);
                appDbContext.SaveChanges();
                foreach (var categoryid in updatepostDTO.category_ids)
                {
                    var post_category = new post_category_temp()
                    {
                        post_id = postDomain.post_id,
                        category_id = categoryid,
                    };
                    appDbContext.post_category_temp.Add(post_category);
                    appDbContext.SaveChanges();
                }
            }
            appDbContext.SaveChanges();
            return updatepostDTO;
        }
        public post? DeletePost(Guid postId)
        {
            var postDomain = appDbContext.post
                .Include(v => v.volume)
                .ThenInclude(c => c.chapter)
                .Include(pt=>pt.post_category_temp)
                .Where(pi => pi.post_id == postId).FirstOrDefault();
            if (postDomain == null)
                return null;
            appDbContext.chapter.RemoveRange(postDomain.volume.SelectMany(v => v.chapter));
            appDbContext.volume.RemoveRange(postDomain.volume);
            appDbContext.post_category_temp.RemoveRange(postDomain.post_category_temp);
            function.DeleteImageFolder(postDomain.post_image);
            appDbContext.post.Remove(postDomain);
            appDbContext.SaveChanges();
            return postDomain;
        }
        public VolumeRequestFormDTO AddVolume(VolumeRequestFormDTO addvolumeDTO)
        {
            var volumeDomain = new volume
            {
                post_id = addvolumeDTO.postId,
                volume_createDate = (DateTime.Now),
                volume_title = addvolumeDTO.volumeTitle,
                volume_description = addvolumeDTO.volumeDescription,
            };
            appDbContext.volume.Add(volumeDomain);
            appDbContext.SaveChanges();
            if (addvolumeDTO.attachFile != null)
                volumeDomain.volume_image = addvolumeDTO.volumeImage = function.UploadImage(addvolumeDTO.attachFile, null, addvolumeDTO.postId, volumeDomain.volume_id, null);
            appDbContext.SaveChanges();
            return addvolumeDTO;
        }
        public VolumeRequestFormDTO UpdateVolume(Guid volumeId, VolumeRequestFormDTO updatevolumeDTO)
        {
            var volumeDomain = appDbContext.volume.Where(v=>v.volume_id == volumeId).FirstOrDefault();
            if (volumeDomain == null)
                return null;
            volumeDomain.volume_title = updatevolumeDTO.volumeTitle;
            volumeDomain.volume_description = updatevolumeDTO.volumeDescription;
            if (updatevolumeDTO.attachFile != null)
                volumeDomain.volume_image = updatevolumeDTO.volumeImage = function.UpdateImage(updatevolumeDTO.attachFile, volumeDomain.volume_image);
            appDbContext.SaveChanges();
            return updatevolumeDTO;
        }
        public volume? DeleteVolume(Guid volumeId)
        {
            var volumeDomain = appDbContext.volume.Include(c => c.chapter).Where(v => v.volume_id == volumeId).FirstOrDefault();
            appDbContext.chapter.RemoveRange(volumeDomain.chapter);
            appDbContext.volume.Remove(volumeDomain);
            function.DeleteImageFolder(volumeDomain.volume_image);
            return volumeDomain;
        }
        public ChapterRequestFormDTO AddChapter(ChapterRequestFormDTO addchapterDTO)
        {
            var chapterDomain = new chapter
            {
                chapter_title = addchapterDTO.chapterTitle,
                chapter_content = addchapterDTO.chapterContent,
                volume_id = addchapterDTO.volumeId,
            };
            appDbContext.chapter.Add(chapterDomain);
            appDbContext.SaveChanges();
            return addchapterDTO;
        }
        public ChapterRequestFormDTO UpdateChapter(Guid chapterId, ChapterRequestFormDTO updatechapterDTO)
        {
            var chapterDomain = appDbContext.chapter.Where(c=>c.chapter_id == chapterId).FirstOrDefault();
            chapterDomain.chapter_title = updatechapterDTO.chapterTitle;
            chapterDomain.chapter_content = updatechapterDTO.chapterContent;
            chapterDomain.volume_id = updatechapterDTO.volumeId = chapterDomain.volume_id;
            appDbContext.SaveChanges();
            return updatechapterDTO;
        }
        public chapter? DeleteChapter(Guid chapterId)
        {
            var chapterDomain = appDbContext.chapter.Include(v=>v.volume).Where(c => c.chapter_id == chapterId).FirstOrDefault();
            function.DeleteChapterImage(chapterDomain.chapter_title, chapterDomain.volume.volume_id, chapterDomain.volume.post_id);
            appDbContext.chapter.Remove(chapterDomain);
            return chapterDomain;
        }
        public ChapterImageRequestFormDTO AddChapterImage(ChapterImageRequestFormDTO addchapterimageDTO)
        {
            if(addchapterimageDTO.attachFile != null)
                addchapterimageDTO.chapterContentImage = function.UploadImage(addchapterimageDTO.attachFile, null, addchapterimageDTO.postId, addchapterimageDTO.volumeId, function.ConvertToUrlFriendlyString(addchapterimageDTO.chapterTitle));
            return addchapterimageDTO;
        }
        public ChapterImageRequestFormDTO UpdateChapterImage(ChapterImageRequestFormDTO updatechapterimageDTO)
        {
            if (updatechapterimageDTO.attachFile != null)
                updatechapterimageDTO.chapterContentImage = function.UpdateImage(updatechapterimageDTO.attachFile, updatechapterimageDTO.chapterContentImage);
            return updatechapterimageDTO;
        }
        public string DeleteChapterImage(string chapterImagePath)
        {
            function.DeleteOneImage(chapterImagePath);
            return chapterImagePath;
        }
        public bool DeleteImages(string imagePath)
        {
            string[] imagePaths = imagePath.Split(';');
            string folderPath = Path.GetDirectoryName(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePaths[0]));
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
