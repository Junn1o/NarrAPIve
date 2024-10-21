using BlogAPI.Model.Domain;
using BlogAPI.Model.DTO;

namespace BlogAPI.Repository.Interface
{
    public interface ICategoryRepository
    {
        List<CategoryDTO> GetAllCategory();
        CategoryWithIdDTO GetCategoryWithIdDTO(Guid categoryId, int pageNumber, int pageSize);
        CategoryRequestFromDTO AddCategory(CategoryRequestFromDTO addcategoryDTO);
        CategoryRequestFromDTO UpdateCategory(Guid categoryId, CategoryRequestFromDTO updatecategoryDTO);
        category DeleteCategory(Guid categoryId);
    }
}
