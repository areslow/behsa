using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Application.Posts;
using bghBackend.Domain.Entities;
using bghBackend.Infra.Utilities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace bghBackend.Infra.Services.Posts
{
    public class PostQueries : IPostQueries
    {
        private readonly ApplicationDbContext _db;

        public PostQueries(ApplicationDbContext db)
        {
            _db = db;
        }

        private async Task<List<PostDTO>> GetAll()
        {
            return await _db.Posts.Where(p => p.IsDeleted == false).Select(p => new PostDTO
            {
                Abstract = p.Abstract,
                AuthorId = p.AuthorId,
                Content = p.Content,
                DateCreated = p.DateCreated,
                Id = p.Id,
                IsOpen = p.IsOpen,
                References = p.References,
                Title = p.Title,
            }).ToListAsync();

        }

        /// <summary>
        /// return an ApiResponse object. success if any object was found , else a failure respons
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse> GetAllPosts()
        {
            try
            {
                List<PostDTO> posts = await GetAll();
                if (posts.Count == 0) return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, posts);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        /// <summary>
        /// get a single post by its id
        /// </summary>
        /// <param name="id">id of the post to be found</param>
        /// <returns></returns>
        public async Task<ApiResponse> GetPostById(long id)
        {
            try
            {
                Post? post = await _db.Posts.FindAsync(id);
                if (post == null || post.IsDeleted == true) return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                PostDTO postToreturn = new()
                {
                    Abstract = post.Abstract,
                    AuthorId = post.AuthorId,
                    Content = post.Content,
                    DateCreated = post.DateCreated,
                    Title = post.Title,
                    Id = post.Id,
                    IsOpen = post.IsOpen,
                    References = post.References,
                };
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, postToreturn);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        /// <summary>
        /// get list of all posts in a certain date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetPostsByDate(string date)
        {
            try
            {
                List<PostDTO> posts = await _db.Posts
                .Where(p => p.IsDeleted == false && DateTime.Parse(p.DateCreated).ToString("yyyy/MM/dd") == DateTime.Parse(date).ToString("yyyy/MM/dd"))
                .Select(p => new PostDTO
                {
                    Abstract = p.Abstract,
                    AuthorId = p.AuthorId,
                    Content = p.Content,
                    DateCreated = p.DateCreated,
                    Id = p.Id,
                    IsOpen = p.IsOpen,
                    References = p.References,
                    Title = p.Title,
                })
                .ToListAsync();
                if (posts == null || posts.Count == 0) return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, posts);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        /// <summary>
        /// get all posts between two specified date
        /// startDate must be smaller than endDate unless it returns nothing even if there was some posts between provided date
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetPostsByDate(string startDate, string endDate)
        {
            try
            {
                List<PostDTO> posts = await _db.Posts
                .Where(p => p.IsDeleted == false && (DateTime.Parse(p.DateCreated) >= DateTime.Parse(startDate) && DateTime.Parse(p.DateCreated) <= DateTime.Parse(endDate)))
                .Select(p => new PostDTO
                {
                    Abstract = p.Abstract,
                    AuthorId = p.AuthorId,
                    Content = p.Content,
                    DateCreated = p.DateCreated,
                    Id = p.Id,
                    IsOpen = p.IsOpen,
                    References = p.References,
                    Title = p.Title,
                })
                .ToListAsync();
                if (posts == null || posts.Count == 0) return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, posts);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        /// <summary>
        /// get all posts thats their title includes the given value
        /// </summary>
        /// <param name="title">the tiltle which posts get compared to</param>
        /// <returns></returns>
        public async Task<ApiResponse> GetPostsByTitle(string title)
        {
            try
            {
                List<PostDTO> posts = await _db.Posts
                .Where(p => p.IsDeleted == false && p.Title.Contains(title))
                .Select(p => new PostDTO
                {
                    Abstract = p.Abstract,
                    AuthorId = p.AuthorId,
                    Content = p.Content,
                    DateCreated = p.DateCreated,
                    Id = p.Id,
                    IsOpen = p.IsOpen,
                    References = p.References,
                    Title = p.Title,
                })
                .ToListAsync();
                if (posts == null || posts.Count == 0) return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, posts);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        /// <summary>
        /// returns all posts that a specific user has created
        /// </summary>
        /// <param name="userId">id of the user his posts need to be found</param>
        /// <returns></returns>
        public async Task<ApiResponse> GetUserPosts(string userId)
        {
            try
            {
                List<PostDTO> posts = await _db.Posts
                .Where(p => p.IsDeleted == false && p.AuthorId == userId)
                .Select(p => new PostDTO
                {
                    Abstract = p.Abstract,
                    AuthorId = p.AuthorId,
                    Content = p.Content,
                    DateCreated = p.DateCreated,
                    Id = p.Id,
                    IsOpen = p.IsOpen,
                    References = p.References,
                    Title = p.Title
                })
                .ToListAsync();
                if (posts == null || posts.Count == 0) return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, posts);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

    }
}
