using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Application.CouncilRequests;
using bghBackend.Infra.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bghBackend.Controllers
{
    [Route("api/ContactInfo")]
    [ApiController]
    public class ContactFormController : ControllerBase
    {
        private readonly ICouncilRequestCommands _requestCommands;
        private readonly ICouncilRequestQueries _requestQueries;

        public ContactFormController(ICouncilRequestCommands requestCommands,
                                     ICouncilRequestQueries requestQueries)
        {
            _requestCommands = requestCommands;
            _requestQueries = requestQueries;
        }

        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetAllRequest()
        {
            return await _requestQueries.GetAllRequests();
        }

        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpGet("{date}")]
        public async Task<ActionResult<ApiResponse>> GetRequestsByDate(string date)
        {
            return await _requestQueries.GetRequestsByDate(date);
        }

        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpGet("{startDate},{endDate}")]
        public async Task<ActionResult<ApiResponse>> GetRequestsBeterrnDates(string startDate, string endDate)
        {
            return await _requestQueries.GetRequestsBetweenDates(startDate, endDate);
        }


        [HttpPost]
        public async Task<IActionResult> RegisterInfo([FromForm] CouncilRequestDTO request)
        {
            var result = await _requestCommands.CreateRequest(request);
            if (result.IsSuccess) return Ok(result);
            else return BadRequest(result);
        }

        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpPut("{requestId:long}+{newState}")]
        public async Task<ActionResult<ApiResponse>> UpdateRequestState(long requestId, string newState)
        {
            return await _requestCommands.UpdateRequestState(requestId, newState);
        }

        [Authorize(Roles = SD.ROLE_ADMIN)]
        [HttpDelete("{requestId:long}")]
        public async Task<ActionResult<ApiResponse>> DeleteRequest(long requestId)
        {
            return await _requestCommands.DeleteRequest(requestId);
        }

    }
}
