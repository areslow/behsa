using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.Auth
{
    public interface IAuthManager
    {
        public Task<ApiResponse> Login(LoginRequestDTO request);
        public Task<ApiResponse> Logout(UserDTO user);
        public Task<ApiResponse> CreateUser(RegisterRequestDTO regRequest);
    }
}
