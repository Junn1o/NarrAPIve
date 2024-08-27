using BlogAPI.Data;
using BlogAPI.Model.Domain;
using BlogAPI.Model.DTO;
using BlogAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly Function function;
        private readonly IConfiguration _configuration;
        public List<CategoryDTO> GetAllCategory()
        {
            var categoryDomain = appDbContext.category.Select(c => new CategoryDTO()
            {
                categoryId = c.category_id,
                categoryName = c.category_name,
            }).ToList();
            return categoryDomain;
        }
        public CategoryWithIdDTO GetCategoryWithIdDTO(Guid categoryId, int pageNumber = 1, int pageSize = 10)
        {
            var categoryDomain = appDbContext.post_category_temp
                .Include(c => c.category)
                .Include(p => p.post).ThenInclude(v => v.volume).ThenInclude(c => c.chapter)
                .Include(p => p.post).ThenInclude(u => u.user)
                .Where(n => n.category_id == categoryId);
            var getCategoryPost = categoryDomain.Select(c => new CategoryWithIdDTO.PostListDTO()
            {
                postId = c.post_id,
                userId = c.post.user_id,
                postTitle = c.post.post_title,
                postCreateDate = c.post.post_createDate.ToString("dd/MM/yyyy"),
                postDescription = c.post.post_description.ToString(),
                firstName = c.post.user.user_firstName,
                lastName = c.post.user.user_lastName,
                postStatus = c.post.post_hidden == true ? "Approved" : "Not approve",
                postType = c.post.post_type == true ? "Blog" : "Light Novel",
                postHidden = c.post.post_hidden == null ? "Hidden" : (c.post.post_hidden == true ? "Public" : "Private"),
                totalVolume = c.post.volume.Count(),
                volumeList = c.post.volume.Select(v => v.volume_title).ToList(),
                categoryName = c.post.post_category_temp.Select(pc => pc.category.category_name).ToList()
            }).AsSplitQuery();
            var skipResults = (pageNumber - 1) * pageSize;
            if (getCategoryPost == null)
            {
                return null;
            }
            else
            {
                var postlist = getCategoryPost.Skip(skipResults).Take(pageSize).ToList();
                var totalResult = getCategoryPost.Count();
                var totalPage = (int)Math.Ceiling((double)totalResult / pageSize);
                var result = new CategoryWithIdDTO
                {
                    post = postlist.ToList(),
                    totalResult = totalResult,
                    totalPages = totalPage,
                };
                return result;
            }
        }
        public CategoryRequestFromDTO AddCategory(CategoryRequestFromDTO addcategoryDTO)
        {
            var categoryDomain = new category
            {
                category_name = addcategoryDTO.categoryName,
            };
            appDbContext.category.Add(categoryDomain);
            appDbContext.SaveChanges();
            return addcategoryDTO;
        }
        public CategoryRequestFromDTO UpdateCategory(Guid categoryId, CategoryRequestFromDTO updatecategoryDTO)
        {
            var categoryDomain = appDbContext.category.FirstOrDefault(ui => ui.category_id == categoryId);
            if (categoryDomain == null)
                return null;
            categoryDomain.category_name = updatecategoryDTO.categoryName;
            appDbContext.SaveChanges();
            return updatecategoryDTO;
        }
        public category DeleteCategory(Guid categoryId)
        {
            var categoryDomain = appDbContext.category.Include(c=>c.post_category_temp).FirstOrDefault(c=>c.category_id == categoryId);
            if (categoryDomain == null)
                return null;
            appDbContext.post_category_temp.RemoveRange(categoryDomain.post_category_temp);
            appDbContext.category.Remove(categoryDomain);
            appDbContext.SaveChanges();
            return categoryDomain;
        }
    }
}
