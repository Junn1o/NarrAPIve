using BlogAPI.Data;
using BlogAPI.Model.Domain;
using BlogAPI.Model.DTO;
using BlogAPI.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
        public UserResultDTO UserList(int pageNumber, int pageSize)
        {
            var userQuery = appDbContext.user.Select(u => new UserResultDTO.UserListDTO()
            {
                userId = u.user_id,
                firstName = u.user_firstName,
                lastName = u.user_lastName,
                avatar = u.user_avatar,
                birthdate = u.user_birthdate.ToString("dd/MM/yyyy"),
                gender = u.user_gender == null ? "Khác" : (u.user_gender == true ? "Nam" : "Nữ")
            }).AsSplitQuery();
            var skipResults = (pageNumber - 1) * pageSize;
            if (userQuery == null)
            {
                return null;
            }
            else
            {
                var userList = userQuery.Skip(skipResults).Take(pageSize).ToList();
                var totalResult = userQuery.Count();
                var totalPage = (int)Math.Ceiling((double)totalResult / pageSize);
                var result = new UserResultDTO
                {
                    user = userList.ToList(),
                    totalResult = totalResult,
                    totalPages = totalPage,
                };
                return result;
            }
        }
        public UserWithIdDTO UserWithId(Guid userId, Guid currentUserId)
        {
            var userDomain = appDbContext.user
                .Include(c=>c.credential)
                .ThenInclude(r=>r.role)
                .Where(u=>u.user_id==userId)
                .Select(u => new UserWithIdDTO()
                {
                    userId = u.user_id,
                    firstName = u.user_firstName,
                    lastName = u.user_lastName,
                    avatar = u.user_avatar,
                    birthdate = u.user_birthdate.ToString("dd/MM/yyyy"),
                    gender = u.user_gender == null ? "Khác" : (u.user_gender == true ? "Nam" : "Nữ"),
                    dateCreate = u.credential.cred_createDate.ToString("dd/MM/yyyy"),
                    roleName = u.credential.role.role_name,
                    userName = userId == currentUserId ? u.credential.cred_userName : "Không có quyền xem",
                }).FirstOrDefault();
            return userDomain;
        }

        public UserRequestFormDTO RegisterUser(UserRequestFormDTO adduserDTO)
        {
            var userDomain = new user
            {
                user_firstName = adduserDTO.firstName,
                user_lastName = adduserDTO.lastName,
                user_birthdate = DateTime.ParseExact(adduserDTO.birthdate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                user_gender = adduserDTO.gender,
            };
            appDbContext.user.Add(userDomain);
            appDbContext.SaveChanges();
            var encryptPassword = function.HashPassword(adduserDTO.password);
            var getRoleID = appDbContext.role
                .Where(r => r.role.role_name == "User")
                .Select(r => r.role.role_id)
                .FirstOrDefault();
            var credDomain = new credential
            {
                user_id = userDomain.user_id,
                cred_createDate = DateTime.Now,
                cred_userName = adduserDTO.userName,
                cred_password = encryptPassword,
                cred_roleid = getRoleID,
            };
            if(adduserDTO.attachFile != null)
                userDomain.user_avatar = UploadImage(adduserDTO.attachFile, userDomain.user_id);
            appDbContext.credential.Add(credDomain);
            appDbContext.SaveChanges();
            return adduserDTO;
        }
        public UserRequestFormDTO UpdateUser(Guid userId,UserRequestFormDTO adduserDTO)
        {
            var userDomain = appDbContext.user.FirstOrDefault(ui => ui.user_id == userId);
            if(userDomain == null)
                return null;
            userDomain.user_id = adduserDTO.userId;
            userDomain.user_lastName = adduserDTO.lastName;
            userDomain.user_firstName = adduserDTO.firstName;
            userDomain.user_birthdate = DateTime.ParseExact(adduserDTO.birthdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (adduserDTO.password != null)
                userDomain.credential.cred_password = function.HashPassword(adduserDTO.password);
            else
                userDomain.credential.cred_password = userDomain.credential.cred_password;
            userDomain.credential.cred_userName = adduserDTO.userName;
            userDomain.user_gender = adduserDTO.gender;
            if (adduserDTO.avatar != null)
                userDomain.user_avatar = UpdateImage(adduserDTO.attachFile, adduserDTO.avatar, userDomain.user_id);
            appDbContext.SaveChanges();
            return adduserDTO;
        }
        public user DeleteUser(Guid userId)
        {
            var userDomain = appDbContext.user
                                .Include(u => u.post)
                                .ThenInclude(p => p.volume)
                                .ThenInclude(v => v.chapter)
                                .Include(u => u.credential)
                                .FirstOrDefault(ui => ui.user_id == userId);
            if (userDomain == null)
                return null;
            appDbContext.chapter.RemoveRange(userDomain.post.SelectMany(p => p.volume.SelectMany(v => v.chapter)));
            appDbContext.volume.RemoveRange(userDomain.post.SelectMany(p => p.volume));
            appDbContext.post_category_temp.RemoveRange(userDomain.post.SelectMany(p => p.post_category_temp));
            appDbContext.post.RemoveRange(userDomain.post);
            appDbContext.credential.Remove(userDomain.credential);
            DeleteImage(userDomain.user_avatar);
            appDbContext.user.Remove(userDomain);
            appDbContext.SaveChanges();
            return userDomain;
        }
        public LoginDTO Login(string userName)
        {
            var credentialDomain = appDbContext.credential.FirstOrDefault(c => c.cred_userName.ToString() == userName.ToString());
            if (credentialDomain == null)
                return null;
            return new LoginDTO
            {
                userName = userName,
            };
        }
        public ResponseDataDTO ResponseData(string userName)
        {
            var loginData = appDbContext.user
                .Include(c => c.credential)
                .ThenInclude(r => r.role)
                .Where(un => un.credential.cred_userName == userName)
                .Select(u => new ResponseDataDTO()
                {
                    userId = u.user_id,
                    userName = u.credential.cred_userName,
                    avatar = u.user_avatar,
                    birthdate = u.user_birthdate,
                    firstName = u.user_firstName,
                    lastName = u.user_lastName,
                    roleName = u.credential.role.role_name,
                }).FirstOrDefault();
            return loginData;
        }
        public bool ValidatePassword(string userName, string inputPassword)
        {
            var user = appDbContext.credential.SingleOrDefault(c => c.cred_userName == userName);
            if (user == null)
                return false;
            var isPasswordValid = function.VerifyPassword(user.cred_password, inputPassword);
            if (!isPasswordValid)
                return false;
            return true;
        }
        public string GenerateJwtToken(ResponseDataDTO responseDataDTO)
        {
            var privateKey = new RSACryptoServiceProvider();
            privateKey.ImportFromPem(File.ReadAllText(_configuration["Jwt:PrivateKey"]));
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var credentials = new SigningCredentials(new RsaSecurityKey(privateKey), SecurityAlgorithms.RsaSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, responseDataDTO.userId.ToString()),
                new Claim(ClaimTypes.Name, responseDataDTO.userName.ToString()),
                new Claim(ClaimTypes.Surname, responseDataDTO.lastName.ToString()),
                new Claim(ClaimTypes.GivenName, responseDataDTO.firstName.ToString()),
                new Claim(ClaimTypes.DateOfBirth, responseDataDTO.birthdate.ToString("dd/MM/yyyy")),
                new Claim(ClaimTypes.Uri, responseDataDTO.avatar.ToString()),
                new Claim(ClaimTypes.Role, responseDataDTO.roleName)
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials

            );
            return new JwtSecurityTokenHandler().WriteToken(token);
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
