using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Application.Replies;
using bghBackend.Domain.Entities;
using bghBackend.Infra.Utilities;

namespace bghBackend.Infra.Services.Replies
{
    public class ReplyCommands : IReplyCommands
    {
        private readonly ApplicationDbContext _db;

        public ReplyCommands(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// create a reply for a comment
        /// </summary>
        /// <param name="reply">an object containing the content to be created and other info</param>
        /// <returns></returns>
        public async Task<ApiResponse> CreateReply(ReplyDTO reply)
        {
            try
            {
                Reply repToCreate = new()
                {
                    AuthorId = reply.AuthorId,
                    Content = reply.Content!,
                    DateCreated = PersianDateTime.Now(),
                    CommentId = reply.CommentId
                };
                await _db.Replies.AddAsync(repToCreate);
                int res = await _db.SaveChangesAsync();
                if (res == 0) return ApiResponse.FailureResponse(SD.CREATION_FAILED);
                return ApiResponse.SuccessResponse(SD.CREATION_SUCCESSFULL);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }


        /// <summary>
        /// delete a reply
        /// </summary>
        /// <param name="replyID"></param>
        /// <returns></returns>
        public async Task<ApiResponse> DeleteReply(long replyID)
        {
            try
            {
                Reply? replyToDelet = await _db.Replies.FindAsync(replyID);
                if (replyToDelet == null || replyToDelet.IsDeleted == true)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                replyToDelet.IsDeleted = true;
                int res = await _db.SaveChangesAsync();
                if (res == 0) return ApiResponse.FailureResponse(SD.ITEM_NOT_CHANGED);
                return ApiResponse.SuccessResponse(SD.ITEM_REMOVED);

            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        /// <summary>
        /// edit the content of a reply
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        public async Task<ApiResponse> UpdateReply(ReplyDTO reply)
        {
            try
            {
                Reply? replyToUpdate = await _db.Replies.FindAsync(reply.Id);
                if (replyToUpdate == null || replyToUpdate.IsDeleted == true)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                replyToUpdate.Content = reply.Content!;
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
