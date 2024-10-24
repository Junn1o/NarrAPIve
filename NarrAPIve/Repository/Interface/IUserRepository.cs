﻿using NarrAPIve.Model.Domain;
using NarrAPIve.Model.DTO;

namespace NarrAPIve.Repository.Interface
{
    public interface IUserRepository 
    {
        UserResultDTO UserList(int pageNumber, int pageSize);
        UserWithIdDTO UserWithId(Guid userId, Guid currentUserId);
        user? DeleteUser(Guid userId);
        UserRequestFormDTO RegisterUser(UserRequestFormDTO userrequestformDTO);
        UserRequestFormDTO UpdateUser(Guid userId,UserRequestFormDTO userrequestformDTO);
        LoginDTO Login(string userName);
        bool ValidatePassword(string userName, string inputPassword);
        ResponseDataDTO ResponseData(string userName);
        string GenerateJwtToken(ResponseDataDTO responseDataDTO);
    }
}
