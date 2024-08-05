using BlogAPI.Model.DTO;

namespace BlogAPI.Repository.Interface
{
    public interface ICredentialRepository
    {
        CredentialDTO Login(string userName, string password);
        //bool ValidatePassword(string inputPassword, string storedPassword);
        LoginDataDTO LoginData(string userName);
        string GenerateJwtToken(LoginDataDTO LoginData);
    }
}
