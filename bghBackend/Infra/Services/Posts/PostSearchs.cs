using bghBackend.Application.Common.OtherModerls;
using bghBackend.Application.Posts;
using bghBackend.Infra.Utilities;
using Microsoft.EntityFrameworkCore;

namespace bghBackend.Infra.Services.Posts
{
    public class PostSearchs : IPostSearchs
    {
        private readonly ApplicationDbContext _db;

        public PostSearchs(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// search the databas and returns all posts that meets specific criteria
        /// </summary>
        /// <param name="searchText">the text posts will be searched by</param>
        /// <param name="searchObj">a model that determin which parts should be looking to</param>
        /// <param name="searchType">type determin if certain parts should match the exact search param
        /// by default it is "include"</param>
        /// <returns></returns>
        public Task<ApiResponse> SearchPosts(string searchText, SearchModelForContents searchObj, string searchType)
        {
            throw new NotImplementedException();
        }
    }
}
