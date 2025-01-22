using Azure.Core;
using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Application.CouncilRequests;
using bghBackend.Domain.Entities;
using bghBackend.Infra.Utilities;
using System.Net;

namespace bghBackend.Infra.Services.CouncilRequests
{
    public class CouncilRequestCommands : ICouncilRequestCommands
    {
        private readonly ApplicationDbContext _db;

        public CouncilRequestCommands(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ApiResponse> CreateRequest(CouncilRequestDTO request)
        {
            try
            {
                CouncilRequest reqToBeCreated = new()
                {
                    Description = request.Description ?? "USER HAS SENT NO MESSAGE",
                    Name = request.Name,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email ?? "",
                    DatePosted = PersianDateTime.Now(),
                    Status = SD.NO_ACTION,
                };
                await _db.CouncilRequests.AddAsync(reqToBeCreated);
                int res = await _db.SaveChangesAsync();
                if (res == 0)
                    return ApiResponse.FailureResponse(SD.CREATION_FAILED);
                return ApiResponse.SuccessResponse(SD.CREATION_SUCCESSFULL);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse("there is been a problem . contact the developer team please");
            }
        }

        /// <summary>
        /// search for request by the given id and remove it if there is any item with that id
        /// </summary>
        /// <param name="requestId">id of item to be removed</param>
        /// <returns></returns>
        public async Task<ApiResponse> DeleteRequest(long requestId)
        {
            ApiResponse response = new();
            try
            {
                CouncilRequest? requestToRemoved = await _db.CouncilRequests.FindAsync(requestId);
                if (requestToRemoved == null || requestToRemoved.IsDeleted == true)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND, HttpStatusCode.NotFound);
                requestToRemoved.IsDeleted = true;
                int res = await _db.SaveChangesAsync();
                if(res == 0)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_CHANGED);
                return ApiResponse.SuccessResponse(SD.ITEM_REMOVED);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        /// <summary>
        /// updated the "Status" of a request object by new value
        /// </summary>
        /// <param name="request">contains the request object id and th new value for the "Status"</param>
        /// <returns></returns>
        public async Task<ApiResponse> UpdateRequestState(long requestId, string newState)
        {

            ApiResponse response = new();
            try
            {
                CouncilRequest? requestToUpdate = await _db.CouncilRequests.FindAsync(requestId);
                if (requestToUpdate == null || requestToUpdate.IsDeleted == true)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND, HttpStatusCode.NotFound);
                requestToUpdate.Status = newState;
                int res = await _db.SaveChangesAsync();
                if (res == 0)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_CHANGED);
                return ApiResponse.SuccessResponse(SD.ITEM_UPDATED);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }
    }
}
