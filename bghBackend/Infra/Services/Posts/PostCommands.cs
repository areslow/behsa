using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Application.Posts;
using bghBackend.Domain.Entities;
using bghBackend.Infra.Utilities;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace bghBackend.Infra.Services.Posts
{
    public class PostCommands : IPostCommands
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;

        public PostCommands(ApplicationDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        /// <summary>
        /// create a post in data base's "post" table
        /// </summary>
        /// <param name="post">an object containing required details of a post</param>
        /// <returns></returns>
        public async Task<ApiResponse> CreatePost(PostDTO post)
        {
            try
            {
                Post postToAdd = new()
                {
                    Abstract = post.Abstract,
                    AuthorId = post.AuthorId ?? null,
                    Content = post.Content,
                    DateCreated = PersianDateTime.Now(),
                    References = post.References,
                    Title = post.Title,
                    IsOpen = post.IsOpen,
                };
                await _db.Posts.AddAsync(postToAdd);
                int res = await _db.SaveChangesAsync();
                if (res == 0) return ApiResponse.FailureResponse(SD.CREATION_FAILED);
                //else
                return ApiResponse.SuccessResponse(SD.CREATION_SUCCESSFULL);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message + (ex.InnerException?.Message ?? ""));
            }
        }

        /// <summary>
        /// delete a post specified by its id
        /// </summary>
        /// <param name="postId">id of the post to be deleted</param>
        /// <returns></returns>
        public async Task<ApiResponse> DeletePost(long postId)
        {
            try
            {
                Post? postToDelete = await _db.Posts.FindAsync(postId);
                if (postToDelete == null || postToDelete.IsDeleted == true)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND, HttpStatusCode.NotFound);

                // this part check if a user can request to change the item or not
                //AppUser? requester = await _db.Users.FindAsync(requesterId);
                //if (requester == null) return ApiResponse.FailureResponse(SD.ITEM_NOT_CHANGED, HttpStatusCode.Forbidden);

                //IList<string>? roles = await _userManager.GetRolesAsync(requester);
                //if (roles == null && !roles!.Contains(SD.ROLE_ADMIN))

                postToDelete.IsDeleted = true;
                int res = await _db.SaveChangesAsync();
                if (res == 0)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_CHANGED);
                return ApiResponse.SuccessResponse(SD.ITEM_REMOVED);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        /// <summary>
        /// lock a post so that it cant be commented on
        /// </summary>
        /// <param name="postId">the id of post to be locked</param>
        /// <returns></returns>
        public async Task<ApiResponse> TogglePostLock(long postId)
        {
            try
            {
                Post? postToToggleLock = await _db.Posts.FindAsync(postId);
                if (postToToggleLock == null || postToToggleLock.IsDeleted == true)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND, HttpStatusCode.NotFound);
                postToToggleLock.IsOpen = !postToToggleLock.IsOpen;
                int res = await _db.SaveChangesAsync();
                if (res == 0)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_CHANGED);
                return ApiResponse.SuccessResponse(SD.ITEM_UPDATED);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        /// <summary>
        /// update the specified post by new values
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public async Task<ApiResponse> UpdatePost(PostDTO post)
        {
            try
            {
                Post? postToUpdate = await _db.Posts.FindAsync(post.Id);
                if (postToUpdate == null || postToUpdate.IsDeleted == true)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                postToUpdate.References = post.References;
                postToUpdate.Title = post.Title;
                postToUpdate.Abstract = post.Abstract;
                postToUpdate.Content = post.Content;
                postToUpdate.IsOpen = post.IsOpen;
                int res = await _db.SaveChangesAsync();
                if (res == 0) // means no record change has been saved
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_CHANGED);
                return ApiResponse.SuccessResponse(SD.ITEM_UPDATED);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }
    }
}
