using BlogAPI.Data;
using BlogAPI.Model.Domain;
using BlogAPI.Model.DTO;
using BlogAPI.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly Function function;
        private readonly IConfiguration _configuration;
        public UserRepository(AppDbContext _appDbContext, Function function, IConfiguration configuration)
        {
            this.appDbContext = _appDbContext;
            this.function = function;
            this._configuration = configuration;
        }
        public UserRequestFormDTO registeruser(UserRequestFormDTO adduserDTO)
        {
            var userDomain = new user
            {
                user_firstName = adduserDTO.user_firstName,
                user_lastName = adduserDTO.user_lastName,
                user_birthday = DateTime.ParseExact(adduserDTO.user_birthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
            };
            appDbContext.user.Add(userDomain);
            appDbContext.SaveChanges();
            var encryptPassword = function.HashPassword(adduserDTO.cred_password);
            var getRoleID = appDbContext.role
                .Where(r => r.role.role_name == "User")
                .Select(r => r.role.role_id)
                .FirstOrDefault();
            var credDomain = new credential
            {
                user_id = userDomain.user_id,
                cred_createDate = DateTime.Now,
                cred_userName = adduserDTO.cred_userName,
                cred_password = encryptPassword,
                cred_roleid = 2,
            };
            if(adduserDTO.attach_file != null)
            {
                userDomain.user_avatar = UploadImage(adduserDTO.attach_file, userDomain.user_id);
            }
            appDbContext.credential.Add(credDomain);
            appDbContext.SaveChanges();
            return adduserDTO;
        }
        public LoginDataDTO loginData(LoginDataDTO loginDataDTO, string userID)
        {
            var getUser = appDbContext.user.Include(p => p.post).ThenInclude(v => v.volume)
                .ThenInclude(c => c.chapter).Include(c=>c.credential).ThenInclude(r=>r.role).Where(ui => ui.user_id.ToString() == userID);

            var userDomain = getUser.Select(ud => new LoginDataDTO()
            {
                lastName = ud.user_lastName,
                firstName = ud.user_firstName,
                avatar = ud.user_avatar,
                birthDate = ud.user_birthday,
                roleName = ud.credential.role.role_name,
                user_id = ud.user_id,
            }).FirstOrDefault();
            return userDomain;

        }
        public string UploadImage(IFormFile file, Guid userId)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "user", userId.ToString());
            Directory.CreateDirectory(uploadFolderPath);
            var filePath = Path.Combine(uploadFolderPath, "avatar" + fileExtension);
            using (FileStream ms = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(ms);
            }
            return Path.Combine("images", "user", userId.ToString(), "avatar" + fileExtension);
        }
        public string UpdateImage(IFormFile file, string avatarPath, Guid userId)
        {
            var oldFullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", avatarPath);
            if (!File.Exists(oldFullPath))
            {
                var newPath = UploadImage(file, userId);
                return newPath;
            }
            else
            {
                File.Delete(oldFullPath);
                var newPath = UploadImage(file, userId);
                return newPath;
            }
        }
        public bool DeleteImage(string imagePath)
        {
            string parentDirectoryName = Path.GetFileName(Path.GetDirectoryName(imagePath));
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "user", parentDirectoryName);
            if (!Directory.Exists(folderPath))
            {
                return false;
            }
            else
            {
                Directory.Delete(folderPath, true);
                return true;
            }
        }
    }
}
