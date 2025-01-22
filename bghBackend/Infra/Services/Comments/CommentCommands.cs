using bghBackend.Application.Comments;
using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Domain.Entities;
using bghBackend.Infra.Utilities;
using System.Net;

namespace bghBackend.Infra.Services.Comments
{
    public class CommentCommands : ICommentCommands
    {
        private readonly ApplicationDbContext _db;

        public CommentCommands(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse> CreateComment(CommentDTO comment)
        {
            try
            {
                var postFromDb = await _db.Posts.FindAsync(comment.PostId);
                if(postFromDb == null) return ApiResponse.FailureResponse("can not comment on a post that is not exists");
                if(!postFromDb.IsOpen) return ApiResponse.FailureResponse("can't comment on this post");
                Comment commentToBeCreated = new()
                {
                    AuthorId = comment.AuthorId,
                    Content = comment.Content,
                    DateCreated = PersianDateTime.Now(),
                    PostId = comment.PostId,
                };
                _db.Comments.Add(commentToBeCreated);
                int res = await _db.SaveChangesAsync();
                if (res == 0) // that means no record is been created in database
                    return ApiResponse.FailureResponse(SD.CREATION_FAILED);
                //else
                return ApiResponse.SuccessResponse(SD.CREATION_SUCCESSFULL);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse("what is it ?");
            }
        }


        public async Task<ApiResponse> UpdateComment(long commentId, string newComment, UserDTO appUser)
        {
            try
            {
                Comment? commentTobeUpdated = await _db.Comments.FindAsync(commentId);
                // object has not been found
                if (commentTobeUpdated == null || commentTobeUpdated.IsDeleted == true)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND + " check item Id",HttpStatusCode.NotFound);
                if (commentTobeUpdated.AuthorId != appUser.Id && !appUser.Roles!.Contains(SD.ROLE_ADMIN))
                    return ApiResponse.FailureResponse(SD.ACTION_UNAUTHORIZED, HttpStatusCode.Unauthorized);
                commentTobeUpdated.Content = newComment;
                int res = await _db.SaveChangesAsync();
                // no change has been recorded in db, meaning object has not been changed
                if (res == 0) return ApiResponse.FailureResponse(SD.ITEM_NOT_CHANGED);
                // everything was ok ...
                return ApiResponse.SuccessResponse(SD.ITEM_UPDATED);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteComment(long commentId, UserDTO appUser)
        {
            try
            {
                Comment? commentToBeDeleted = await _db.Comments.FindAsync(commentId);
                if(commentToBeDeleted == null || commentToBeDeleted.IsDeleted == true)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND, HttpStatusCode.NotFound);
                if (commentToBeDeleted.AuthorId != appUser.Id && !appUser.Roles!.Contains(SD.ROLE_ADMIN))
                    return ApiResponse.FailureResponse(SD.ACTION_UNAUTHORIZED, HttpStatusCode.Unauthorized);
                commentToBeDeleted.IsDeleted = true;
                int res = await _db.SaveChangesAsync();
                if(res == 0)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_CHANGED);
                return ApiResponse.SuccessResponse(SD.ITEM_REMOVED);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }
    }
}
