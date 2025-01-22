using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.Posts
{
    public interface IPostCommands
    {
        public Task<ApiResponse> CreatePost(PostDTO post);
        public Task<ApiResponse> UpdatePost(PostDTO post);
        public Task<ApiResponse> TogglePostLock(long postId);
        public Task<ApiResponse> DeletePost(long postId);
    }
}
