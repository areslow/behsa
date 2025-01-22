using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.Posts
{
    public interface IPostSearchs
    {
        /// <summary>
        /// return a lists of posts that meet the provided criteria,
        /// if searchType is include it search and returns all the items that includes
        /// </summary>
        /// <param name="searchText">the expression to be searched on data(s)</param>
        /// <param name="searchObj">contains boolean values that specifies whether a specific part should be looked at for the search text
        ///     like title, or author
        /// </param>
        /// <param name="searchType">specifies the search method that is either include or exact match</param>
        /// <returns>list of posts that meet specified criteria</returns>
        public Task<ApiResponse> SearchPosts(string searchText, SearchModelForContents searchObj, string searchType);
    }
}
