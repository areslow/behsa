using bghBackend.Application.Common.OtherModerls;
using bghBackend.Application.Feeds;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;

namespace bghBackend.Controllers
{
    [Route("api/RSS")]
    [ApiController]
    public class RSSController : ControllerBase
    {
        private readonly string? _feedUrl;
        private readonly IFeeds _feeds;
        public RSSController(IConfiguration configuration, IFeeds feeds)
        {
            _feedUrl = configuration.GetValue<string>("RSSUrl:default");
            _feeds = feeds;
        }
        [HttpGet("spiegel")]
        public async Task<ActionResult<ApiResponse>> GetSpiegelRSS()
        {
            return await _feeds.GetFeeds(_feedUrl!);
        }
    }
}
