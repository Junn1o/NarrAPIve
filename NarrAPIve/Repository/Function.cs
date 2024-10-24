using NarrAPIve.Data;
using NarrAPIve.Model.Domain;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;

namespace NarrAPIve.Repository
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
        public string UploadImage(IFormFile? file, Guid? userId, Guid? postId, Guid? volumeId, string? chapterTitle)
        {
            var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            var fileExtension = Path.GetExtension(file.FileName);
            if (postId != Guid.Empty && volumeId == Guid.Empty && userId == Guid.Empty)
            {
                var postFolder = Path.Combine(uploadFolderPath , "post" , postId.ToString());
                Directory.CreateDirectory(postFolder);
                var filePath = Path.Combine(postFolder, "post-base-image" + fileExtension);
                using (FileStream ms = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(ms);
                }
                return filePath;
            }
            // volume only
            if (postId != Guid.Empty && volumeId != Guid.Empty && chapterTitle == null && userId == Guid.Empty)
            {
                var volumeFolder = Path.Combine(uploadFolderPath , "post" , postId.ToString() + volumeId.ToString());
                Directory.CreateDirectory(volumeFolder);
                var filePath = Path.Combine(volumeFolder, "volume-base-image" + fileExtension);
                using (FileStream ms = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(ms);
                }
                return filePath;
            }
            // chapter only
            if (postId != Guid.Empty && volumeId != Guid.Empty && chapterTitle != null && userId == Guid.Empty)
            {
                var volumeFolder = Path.Combine(uploadFolderPath , "post" , postId.ToString() , volumeId.ToString());
                Directory.CreateDirectory(volumeFolder);
                int fileCount = Directory.GetFiles(volumeFolder).Length + 1;
                var filePath = Path.Combine(volumeFolder, chapterTitle + "-" + fileCount + fileExtension);
                using (FileStream ms = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(ms);
                }
                return filePath;
            }
            else
            {
                var userFolder = Path.Combine(uploadFolderPath , "user" , userId.ToString());
                Directory.CreateDirectory(userFolder);
                var filePath = Path.Combine(userFolder, "avatar" + fileExtension);
                using (FileStream ms = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(ms);
                }
                return filePath = Path.Combine("images" , "user" , userId.ToString(), "avatar" + fileExtension);
            }
        }
        public string UpdateImage(IFormFile? file, string filePath)
        {
            if (file == null || string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File or file path cannot be null or empty.");
            }

            var fileExtension = Path.GetExtension(file.FileName);
            var directoryPath = Path.GetDirectoryName(filePath);
            var newFilePath = Path.Combine(directoryPath, Path.GetFileNameWithoutExtension(filePath) + fileExtension);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (FileStream ms = new FileStream(newFilePath, FileMode.Create))
            {
                file.CopyTo(ms);
            }
            return newFilePath;
        }
        public string DeleteImageFolder(string filePath)
        {
            string getFolder = Path.Combine(filePath);
            if (!Directory.Exists(filePath))
            {
                return null;
            }
            else
            {
                Directory.Delete(filePath, true);
                return filePath;
            }
        }
        public string DeleteChapterImage(string chapterTitle, Guid postId, Guid volumeId)
        {
            var volumeFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "post" + postId.ToString() + volumeId.ToString());
            var filesToDelete = Directory.GetFiles(volumeFolder, chapterTitle + "-*");
            foreach (var file in filesToDelete)
            {
                File.Delete(file);
            }
            return volumeFolder;
        }
        public string DeleteOneImage(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            else
            {
                File.Delete(filePath);
                return filePath;
            }
        }
        public string ConvertToUrlFriendlyString(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString))
            {
                return string.Empty;
            }
            string urlFriendly = inputString.Trim().Replace(" ", "-");
            urlFriendly = System.Text.RegularExpressions.Regex.Replace(urlFriendly, @"[^a-zA-Z0-9\-]", "");
            return urlFriendly;
        }
    }
}
