using BlogAPI.Model.DTO;

namespace BlogAPI.Repository.Interface
{
    public interface ICredentialRepository
    {
        CredentialDTO Login(string userName);
        bool ValidatePassword(string userName, string inputPassword);
        LoginDataDTO LoginData(string userName);
        string GenerateJwtToken(LoginDataDTO LoginData);
    }
}
