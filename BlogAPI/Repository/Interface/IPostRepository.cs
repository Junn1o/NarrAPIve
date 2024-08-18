using BlogAPI.Model.DTO;

namespace BlogAPI.Repository.Interface
{
    public interface IPostRepository
    {
        PostResultDTO getfullpost(int pageNumber, int pageSize);
        PostRequestFormDTO addpostDTO(PostRequestFormDTO addpostDTO);
        //postwithidDTO Getpostwithid(Guid id);
    }
}
