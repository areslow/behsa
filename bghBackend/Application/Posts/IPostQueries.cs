using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.Posts
{
    public interface IPostQueries
    {
        /// <summary>
        /// get all posts and their details from database
        /// </summary>
        /// <returns>all posts from database</returns>
        public Task<ApiResponse> GetAllPosts();

        /// <summary>
        /// return the list of all post created by a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<ApiResponse> GetUserPosts(string userId);
        /// <summary>
        /// return a single post that has the given id
        /// </summary>
        /// <param name="id">id of the post to be returned</param>
        /// <returns>a post object or null</returns>
        public Task<ApiResponse> GetPostById(long id);

        /// <summary>
        /// find and returns a list of post that their title includes the given title
        /// </summary>
        /// <param name="title">the title which a desired post should include</param>
        /// <returns></returns>
        public Task<ApiResponse> GetPostsByTitle(string title);
        /// <summary>
        /// return list of all posts created in the given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public Task<ApiResponse> GetPostsByDate(string date);
        /// <summary>
        /// return list of all posts created between two given dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Task<ApiResponse> GetPostsByDate(string startDate, string endDate);
    }
}
