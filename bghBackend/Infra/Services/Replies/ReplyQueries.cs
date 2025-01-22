using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Application.Replies;
using bghBackend.Infra.Utilities;
using Microsoft.EntityFrameworkCore;

namespace bghBackend.Infra.Services.Replies
{
    public class ReplyQueries : IReplyQueries
    {
        private readonly ApplicationDbContext _db;

        public ReplyQueries(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// get all replies of a comment by the comment id
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetRepliesByCommentId(long commentId)
        {
            try
            {
                List<ReplyDTO> replies = await _db.Replies.Where(r => r.CommentId == commentId && r.IsDeleted == false).Include(r => r.Author).Select(r => new ReplyDTO
                {
                    AuthorId = r.AuthorId,
                    CommentId = r.CommentId,
                    Content = r.Content,
                    DateCreated = r.DateCreated,
                    AuthorName = r.Author == null? SD.ANONYMOUS_USER : r.Author.FirstName + " " + r.Author.LastName,
                }).ToListAsync();
                if (replies.Count == 0 || replies == null)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, replies);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }
    }
}
