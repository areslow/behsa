using bghBackend.Application.Common.OtherModerls;
using bghBackend.Application.Feeds;
using System.ServiceModel.Syndication;
using System.Xml;

namespace bghBackend.Infra.Services.Feeds
{
    public class Feeds : IFeeds
    {
        public async Task<ApiResponse> GetFeeds(string url)
        {
            try
            {
                using var reader = XmlReader.Create(url);
                var feed = SyndicationFeed.Load(reader);

                return ApiResponse.SuccessResponse("feeds fetched", feed);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse("failed to fetch feeds : " + ex.Message);
            }
        }
    }
}
