using Microsoft.AspNetCore.Identity;

namespace BlogAPI.Repository.Interface
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
