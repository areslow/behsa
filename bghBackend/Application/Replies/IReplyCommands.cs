using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.Replies
{
    public interface IReplyCommands
    {
        public Task<ApiResponse> CreateReply(ReplyDTO reply);
        public Task<ApiResponse> UpdateReply(ReplyDTO reply);
        public Task<ApiResponse> DeleteReply(long replyID);
    }
}
