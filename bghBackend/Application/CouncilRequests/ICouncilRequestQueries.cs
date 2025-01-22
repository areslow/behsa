using bghBackend.Application.Common.OtherModerls;

namespace bghBackend.Application.CouncilRequests
{
    public interface ICouncilRequestQueries
    {
        /// <summary>
        /// return the list of all request that have been sent to system
        /// </summary>
        /// <returns></returns>
        public Task<ApiResponse> GetAllRequests();
        public Task<ApiResponse> GetRequestsByDate(string date);
        public Task<ApiResponse> GetRequestsBetweenDates(string startDate, string endDate);
    }
}
