using BlogAPI.Model.DTO;

namespace BlogAPI.Repository.Interface
{
    public interface IPostRepository
    {
        PostResultDTO getfullpost(int pageNumber, int pageSize);
        PostrequestFormDTO addpostDTO(PostrequestFormDTO addpostDTO);
        //postwithidDTO Getpostwithid(Guid id);
    }
}
