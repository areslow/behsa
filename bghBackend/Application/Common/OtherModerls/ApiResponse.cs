using System.Net;

namespace bghBackend.Application.Common.OtherModerls
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
        public object? Result { get; set; } = new();

        /// <summary>
        /// create an ApiResponse object that has successfull result
        /// </summary>
        /// <param name="messages">List of messages to be returned</param>
        /// <param name="result">The result data if there is any. by default returns null</param>
        /// <param name="status">Http status code to return. by default its HttpStatusCode.OK</param>
        /// <returns></returns>
        public static ApiResponse SuccessResponse(string message, object? result = null, HttpStatusCode status = HttpStatusCode.OK)
        {
            return new ApiResponse
            {
                StatusCode = status,
                IsSuccess = true,
                Message = message,
                Result = result
            };
        }

        /// <summary>
        /// create an ApiResponse object with failure statuse
        /// by default it returns "BadRequest" statuse code 
        /// </summary>
        /// <param name="messages">list of messages to be returned.</string></param>
        /// <param name="status">HttpStatusCode to be sent</param>
        /// <returns></returns>
        public static ApiResponse FailureResponse(string message, HttpStatusCode status = HttpStatusCode.BadRequest)
        {
            return new ApiResponse
            {
                StatusCode = status,
                IsSuccess = false,
                Message = message,
                Result = null
            };
        }
    }
}
