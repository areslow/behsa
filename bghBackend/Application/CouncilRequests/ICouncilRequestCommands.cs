using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.CouncilRequests
{
    public interface ICouncilRequestCommands
    {
        public Task<ApiResponse> CreateRequest(CouncilRequestDTO request);
        public Task<ApiResponse> DeleteRequest(long requestId);// admin only
        public Task<ApiResponse> UpdateRequestState(long requestId, string newState);
    }
}
