using BlogAPI.Model.DTO;

namespace BlogAPI.Repository.Interface
{
    public interface IUserRepository 
    {
        UserRequestFormDTO registeruser(UserRequestFormDTO userrequestformDTO); 
        //LoginDataDTO loginData(LoginDataDTO logindataDTO, string userID);
    }
}
