using bghBackend.Application.AppUser;
using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Domain.Entities;
using bghBackend.Infra.Utilities;

namespace bghBackend.Infra.Services.Users
{
    public class UserCommands : IUserCommands
    {
        private readonly ApplicationDbContext _db;

        public UserCommands(ApplicationDbContext db)
        {
            _db = db;
        }

        //public async Task<ApiResponse> CreateUser(UserDTO user)
        //{
        //    try
        //    {
        //        AppUser userToCreate = new()
        //        {
        //            UserName = user.UserName,
        //            Email = user.Email,
        //            PhoneNumber = user.PhoneNumber,
        //            FirstName = user.FirstName,
        //            LastName = user.LastName,
        //        };
        //        await _db.Users.AddAsync(userToCreate);
        //        int res = await _db.SaveChangesAsync();
        //        if (res == 0) return ApiResponse.FailureResponse(SD.CREATION_FAILED);
        //        return ApiResponse.SuccessResponse(SD.CREATION_SUCCESSFULL);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiResponse.FailureResponse(ex.Message);
        //    }
        //}

        public async Task<ApiResponse> DeleteUser(string userId)
        {
            try
            {
                AppUser? userToDelete = await _db.Users.FindAsync(userId);
                if (userToDelete == null || userToDelete.IsDeleted == true)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                userToDelete.IsDeleted = true;
                int res = await _db.SaveChangesAsync();
                if (res == 0) return ApiResponse.FailureResponse(SD.ITEM_NOT_CHANGED);
                return ApiResponse.SuccessResponse(SD.ITEM_REMOVED);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateUser(UserDTO user)
        {
            try
            {
                AppUser? userToUpdate = await _db.Users.FindAsync(user.Id);
                if (userToUpdate == null || userToUpdate.IsDeleted == true)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                // we can set which property should be or can be changed
                // for now asuming only 3 property will change
                userToUpdate.UserName += Guid.NewGuid().ToString();
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.Email = user.Email;
                // if the user was returned AsNoTracking then we should perform the _db.Update or UpdateAsync
                int res = await _db.SaveChangesAsync();
                if (res == 0) return ApiResponse.FailureResponse(SD.ITEM_NOT_CHANGED);
                return ApiResponse.SuccessResponse(SD.ITEM_UPDATED);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }
    }
}
