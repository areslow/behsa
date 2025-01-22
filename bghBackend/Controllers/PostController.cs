using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Application.Posts;
using bghBackend.Infra.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bghBackend.Controllers
{
    /// <summary>
    /// Article is the same as Post . just named the controller to Article so that we do not confuse the nam Post with other contents
    /// </summary>
    [Route("api/Post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostQueries _postQueries;
        private readonly IPostCommands _postCommands;

        public PostController(IPostCommands postCommands, IPostQueries psotQueries)
        {
            _postCommands = postCommands;
            _postQueries = psotQueries;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllPosts()
        {
            return await _postQueries.GetAllPosts();
        }

        [HttpGet("{postId:long}")]
        public async Task<ActionResult<ApiResponse>> GetPostById(long postId)
        {
            return await _postQueries.GetPostById(postId);
        }

        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreatePost([FromBody]PostDTO post) 
        {
            return await _postCommands.CreatePost(post);
        }

        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpPut]
        //posts body is a json object from RTE output and shoud be converted to proper format in front to be displayed to the end user
        public async Task<ActionResult<ApiResponse>> EditPost([FromBody] PostDTO post)
        {
            return await _postCommands.UpdatePost(post);
        }

        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpPut("{postId:long}")]
        public async Task<ActionResult<ApiResponse>> TogglePostLoack(long postId)
        {
            return await _postCommands.TogglePostLock(postId);
        }


        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpDelete("{postId:long}")]
        public async Task<ActionResult<ApiResponse>> DeletePost(long postId)
        {
            return await _postCommands.DeletePost(postId);
        }
        

    }
}
