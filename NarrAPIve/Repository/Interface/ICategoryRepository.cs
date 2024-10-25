using NarrAPIve.Model.Domain;
using NarrAPIve.Model.DTO;

namespace NarrAPIve.Repository.Interface
{
    public interface ICategoryRepository
    {
        List<CategoryDTO> GetAllCategory();
        CategoryWithIdDTO GetCategoryWithIdDTO(int categoryId, int pageNumber, int pageSize);
        CategoryRequestFromDTO AddCategory(CategoryRequestFromDTO addcategoryDTO);
        CategoryRequestFromDTO UpdateCategory(int categoryId, CategoryRequestFromDTO updatecategoryDTO);
        category DeleteCategory(int categoryId);
    }
}
