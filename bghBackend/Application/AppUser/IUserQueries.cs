using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.AppUser
{
    public interface IUserQueries
    {
        public Task<ApiResponse> GetAllUser();
        public Task<ApiResponse> GetUserById(string userId);
        public Task<ApiResponse> SearchUsers(string searchText);
    }
}
