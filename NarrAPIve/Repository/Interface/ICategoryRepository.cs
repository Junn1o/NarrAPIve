using NarrAPIve.Model.Domain;
using NarrAPIve.Model.DTO;

namespace NarrAPIve.Repository.Interface
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
