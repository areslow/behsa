using bghBackend.Application.Comments;
using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bghBackend.Controllers
{
    [Route("api/Comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly ICommentQueries _commentQueries;
        private readonly ICommentCommands _commentCommands;

        public CommentController(ICommentQueries commentQueries, ICommentCommands commentCommands)
        {
            _commentQueries = commentQueries;
            _commentCommands = commentCommands;
        }

        [HttpGet("{postId:long}")]
        public async Task<ActionResult<ApiResponse>> getPostComments(long postId)
        {
            return await _commentQueries.GetCommentByPostId(postId);
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateComment([FromBody]CommentDTO comment)
        {
            return await _commentCommands.CreateComment(comment);
        }

        [Authorize]
        [HttpPut("{cmtId:long}")]
        public async Task<ActionResult<ApiResponse>> UpdateComment(long cmtId,[FromBody]UpdateCommentRequestDTO updateRequest)
        {
            return await _commentCommands.UpdateComment(cmtId, updateRequest.NewComment, updateRequest.AppUser);
        }

        [Authorize]
        [HttpDelete("{cmtId:long}")]
        public async Task<ActionResult<ApiResponse>> DeleteComment(long cmtId, [FromBody] UserDTO appUser)
        {
            return await _commentCommands.DeleteComment(cmtId, appUser);
        }
    }
}
