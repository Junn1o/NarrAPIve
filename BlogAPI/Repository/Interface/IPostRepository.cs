using BlogAPI.Model.DTO;

namespace BlogAPI.Repository.Interface
{
    public interface IPostRepository
    {
        postresultDTO getfullpost(int pageNumber, int pageSize);
        postrequestformDTO addpostDTO(postrequestformDTO addpostDTO);
        //postwithidDTO Getpostwithid(Guid id);
    }
}
