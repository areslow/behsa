using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.Comments
{
    public interface ICommentCommands
    {
        public Task<ApiResponse> CreateComment(CommentDTO comment);
        public Task<ApiResponse> UpdateComment(long commentId, string newComment, UserDTO appUser);
        public Task<ApiResponse> DeleteComment(long commentId, UserDTO appUser);
    }
}
