using BlogAPI.Data;
using Microsoft.AspNetCore.Identity;

namespace BlogAPI.Repository
{
    public class Function
    {
        private readonly PasswordHasher<object> passwordHasher;
        public Function()
        {
            passwordHasher = new PasswordHasher<object>();
        }

        public string HashPassword(string password)
        {
            return passwordHasher.HashPassword(null, password);
        }
        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}
