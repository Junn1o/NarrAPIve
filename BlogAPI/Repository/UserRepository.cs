using BlogAPI.Data;
using BlogAPI.Model.Domain;
using BlogAPI.Model.DTO;
using BlogAPI.Repository.Interface;
using System.Globalization;

namespace BlogAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appDbContext;
        public UserRepository(AppDbContext _appDbContext)
        {
            this.appDbContext = _appDbContext;
        }
        public userrequestformDTO adduserDTO(userrequestformDTO adduserDTO)
        {
            var userDomain = new user
            {
                user_firstName = adduserDTO.user_firstName,
                user_lastName = adduserDTO.user_lastName,
                user_birthDate = DateTime.ParseExact(adduserDTO.user_birthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
            };
            appDbContext.user.Add(userDomain);
            appDbContext.SaveChanges();
            var credDomain = new credential
            {
                user_id = userDomain.user_id,
                cred_createDate = DateTime.Now,
                cred_userName = adduserDTO.cred_userName,
                cred_passWord = adduserDTO.cred_passWord,
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
