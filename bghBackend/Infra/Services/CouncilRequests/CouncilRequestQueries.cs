using Azure;
using bghBackend.Application.Common.DTOs;
using bghBackend.Application.Common.OtherModerls;
using bghBackend.Application.CouncilRequests;
using bghBackend.Infra.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace bghBackend.Infra.Services.CouncilRequests
{
    public class CouncilRequestQueries : ICouncilRequestQueries
    {

        private readonly ApplicationDbContext _db;

        public CouncilRequestQueries(ApplicationDbContext db)
        {
            _db = db;
        }

        private async Task<List<CouncilRequestDTO>> GetAll()
        {
            var res =  await _db.CouncilRequests.Where(cr => cr.IsDeleted == false).Select(rq => new CouncilRequestDTO
            {
                Description = rq.Description,
                Id = rq.Id,
                IsRead = rq.IsRead,
                Name = rq.Name,
                PhoneNumber = rq.PhoneNumber,
                Status = rq.Status,
            }).ToListAsync();
            return res;
        }
        public async Task<ApiResponse> GetAllRequests()
        {
            try
            {
                List<CouncilRequestDTO> requests = await GetAll();
                if(requests.Count == 0)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND, HttpStatusCode.NotFound);
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, requests);
            }
            catch (Exception ex) 
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetRequestsBetweenDates(string startDate = "", string endDate = "")
        {
            ApiResponse response = new();
            try
            {
                List<CouncilRequestDTO> requests = await GetAll();
                if (string.IsNullOrEmpty(startDate))
                    requests = requests.Where(rq => DateTime.Parse(rq.DatePosted) <= DateTime.Parse(endDate)).ToList();
                else if (string.IsNullOrEmpty(endDate))
                    requests = requests.Where(rq => DateTime.Parse(rq.DatePosted) >= DateTime.Parse(startDate)).ToList();
                else if (string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
                    return ApiResponse.FailureResponse("cant send two empty params");
                else
                    requests = requests.Where(rq => DateTime.Parse(rq.DatePosted) >= DateTime.Parse(startDate) && DateTime.Parse(rq.DatePosted) <= DateTime.Parse(endDate)).ToList();
                if (requests.Count == 0)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND);
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, requests);
            }
            catch (Exception ex) 
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetRequestsByDate(string date)
        {
            try
            {
                List<CouncilRequestDTO> requests = await GetAll();
                requests = requests.Where(rq => DateTime.Parse(rq.DatePosted) == DateTime.Parse(date)).ToList();
                if (requests.Count == 0)
                    return ApiResponse.FailureResponse(SD.ITEM_NOT_FOUND, HttpStatusCode.NotFound);
                return ApiResponse.SuccessResponse(SD.RETRIVED_SUCCESSFULLY, requests);
            }
            catch (Exception ex)
            {
                return ApiResponse.FailureResponse(ex.Message);
            }
        }
    }
}
