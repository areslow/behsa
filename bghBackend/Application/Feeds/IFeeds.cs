using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.Feeds
{
    public interface IFeeds
    {
        public Task<ApiResponse> GetFeeds(string url);
    }
}
