using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.AppUser
{
    public interface IUserCommands
    {
        //public Task<ApiResponse> CreateUser(UserDTO user);
        public Task<ApiResponse> UpdateUser(UserDTO user);
        public Task<ApiResponse> DeleteUser(string userId);
    }
}
