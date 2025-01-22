using bghBackend.Application.Comments;
using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using System.Net;
using Microsoft.EntityFrameworkCore;
using bghBackend.Infra.Utilities;

namespace bghBackend.Infra.Services.Comments
{
    public class CommentQueries : ICommentQueries
    {
        private readonly ApplicationDbContext _db;

        public CommentQueries(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// return comments of a specific post with the given post id
        /// </summary>
        /// <param name="postId">id of the post which comments belong to</param>
        /// <returns>list of comments of the post</returns>
        public async Task<ApiResponse> GetCommentByPostId(long postId)
        {
            try
            {
                var comments = await _db.Comments.Where(c => c.PostId == postId && c.IsDeleted == false).Include(c => c.Author)
                                            .Include(c => c.Replies).Select(cm => new CommentDTO
                                            {
                                                AuthorId = string.IsNullOrEmpty(cm.AuthorId) ? "" : cm.AuthorId,
                                                AuthorName = cm.Author == null ? "anonymous" : cm.Author.FirstName + cm.Author.LastName,
                                                Content = cm.Content,
                                                DateCreated = cm.DateCreated,
                                                Id = cm.Id,
                                                PostId = cm.PostId,
                                                IsOpen = cm.IsOpen,
                                                Replies = cm.Replies != null ? cm.Replies.Select(rp => new ReplyDTO
                                                {
                                                    AuthorId = rp.AuthorId,
                                                    Content = rp.Content,
                                                    DateCreated = rp.DateCreated,
                                                    Id = rp.Id,
                                                    AuthorName = rp.Author == null ? "anonymous" : rp.Author.FirstName + rp.Author.LastName,
                                                }).ToList() : null,
                                            }).ToListAsync();
                if (comments.Count == 0) return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND, HttpStatusCode.NotFound);
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, comments);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        
    }
}
