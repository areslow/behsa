using bghBackend.Application.AppUser;
using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Domain.Entities;
using bghBackend.Infra.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace bghBackend.Infra.Services.Users
{
    public class UserQueries : IUserQueries
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;

        public UserQueries(ApplicationDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<ApiResponse> GetAllUser()
        {
            try
            {
                List<UserDTO> users = await _userManager.Users.Where(u => u.IsDeleted == false)
                    .Include(u => u.Posts)
                    .AsNoTracking()
                    .Select(u => new UserDTO
                    {
                        Email = u.Email,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Id = u.Id,
                        PhoneNumber = u.PhoneNumber,
                        UserName = u.UserName,
                        Roles =  _userManager.GetRolesAsync(u).Result // need to use "Join" method instead . looking for syntax !!!
                    }).ToListAsync();
                if (users == null || users.Count == 0)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, users);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetUserById(string userId)
        {
            try
            {
                AppUser? userFromDb = await _db.Users.FindAsync(userId);
                if (userFromDb == null || userFromDb.IsDeleted == true)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                UserDTO userToReturn = new()
                {
                    Email = userFromDb.Email,
                    FirstName = userFromDb.FirstName,
                    Id = userFromDb.Id,
                    LastName= userFromDb.LastName,
                    PhoneNumber= userFromDb.PhoneNumber,
                    UserName = userFromDb.UserName,
                    Roles = _userManager.GetRolesAsync(userFromDb).Result
                };
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, userToReturn);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> SearchUsers(string searchText)
        {
            try
            {
                List<UserDTO> users = await _db.Users.AsNoTracking().Select(u => new UserDTO
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    Id = u.Id,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    UserName = u.UserName,
                    Roles = _userManager.GetRolesAsync(u).Result
                }).Where(u => u.FirstName!.ToLower().Contains(searchText.ToLower()) || 
                                                       u.LastName!.ToLower().Contains(searchText.ToLower())  ||
                                                       u.Email!.ToLower().Contains(searchText.ToLower())     ||
                                                       u.UserName!.ToLower().Contains(searchText.ToLower())  ||
                                                       u.PhoneNumber!.ToLower().Contains(searchText.ToLower())).ToListAsync();
                if (users == null || users.Count == 0)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, users);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }
    }
}
