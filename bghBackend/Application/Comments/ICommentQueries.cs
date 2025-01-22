using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.Comments
{
    public interface ICommentQueries
    {
        /// <summary>
        /// get all comments related to a specific post
        /// </summary>
        /// <param name="postId">the post id of which the comments should get returned</param>
        /// <returns>list of comments of the specified post</returns>
        public Task<ApiResponse> GetCommentByPostId(long postId);
    }
}
