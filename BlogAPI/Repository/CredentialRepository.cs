using BlogAPI.Data;
using BlogAPI.Model.Domain;
using BlogAPI.Model.DTO;
using BlogAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace BlogAPI.Repository
{
    public class CredentialRepository : ICredentialRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly Function function;
        private readonly IConfiguration _configuration;
        public CredentialRepository(AppDbContext _appDbContext, Function function, IConfiguration configuration)
        {
            this.appDbContext = _appDbContext;
            this.function = function;
            this._configuration = configuration;
        }

        public CredentialDTO Login(string userName, string password)
        {
            var credentialDomain = appDbContext.credential
                //.Include(r => r.user)
                //.ThenInclude(p => p.post)
                //.ThenInclude(v => v.volume)
                //.ThenInclude(c => c.chapter)
                //.Include(r => r.role)
                .FirstOrDefault(c => c.cred_userName.ToString() == userName.ToString());
            var isPasswordValid = ValidatePassword(userName.ToString(), password.ToString());
            if (!isPasswordValid)
                return null;
            if (credentialDomain == null)
                return null;
            return new CredentialDTO
            {
                userName = userName,
            };
        }
        public LoginDataDTO LoginData(string userName)
        {
            var loginData = appDbContext.user.Include(c => c.credential).ThenInclude(r => r.role).Where(un => un.credential.cred_userName== userName).Select(u => new LoginDataDTO()
            {
                user_id = u.user_id,
                avatar = u.user_avatar,
                birthDate = u.user_birthday,
                firstName = u.user_firstName,
                lastName = u.user_lastName,
                roleName = u.credential.role.role_name,
            }).FirstOrDefault();
            return loginData;

        }
        public bool ValidatePassword(string userName, string providedPassword)
        {
            var user = appDbContext.credential.SingleOrDefault(c => c.cred_userName == userName);
            if (user == null)
            {
                return false;
            }
            return function.VerifyPassword(user.cred_password, providedPassword);
        }
        public string GenerateJwtToken(LoginDataDTO loginDataDTO)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                //new Claim(ClaimTypes.NameIdentifier, credential.user_id.ToString()),
                //new Claim(ClaimTypes.Name, credential.userName.ToString()),
                //new Claim(ClaimTypes.Surname, credential.lastName.ToString()),
                //new Claim(ClaimTypes.GivenName, credential.firstName.ToString()),
                //new Claim(ClaimTypes.DateOfBirth, credential.birthDate.ToString("dd/MM/yyyy")),
                //new Claim(ClaimTypes.Uri, credential.avatar.ToString()),
                //new Claim(ClaimTypes.Role, credential.roleName)
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Issuer"],
                audience: _configuration["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
