using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.Replies
{
    public interface IReplyQueries
    {
        public Task<ApiResponse> GetRepliesByCommentId(long commentId);
    }
}
