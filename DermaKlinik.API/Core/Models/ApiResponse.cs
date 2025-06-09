using Newtonsoft.Json;
using System.Net;

namespace DermaKlinik.API.Core.Models
{
    public class ApiResponse
    {
        public bool Result { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; set; }
        public string? ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty]
        public object Data { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
        public static ApiResponse SuccessResult(object data = null, string message = "İşlem başarılı", HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiResponse
            {
                Result = true,
                Message = message,
                StatusCode = statusCode,
                Data = data
            };
        }

        public static ApiResponse ErrorResult(string message = "İşlem başarısız", HttpStatusCode statusCode = HttpStatusCode.BadRequest, object data = null)
        {
            return new ApiResponse
            {
                Result = false,
                ErrorMessage = message,
                StatusCode = statusCode,
                Data = data
            };
        }
    }

    public class ApiResponse<T>
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public string? ErrorMessage { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty]
        public T Data { get; set; }
        public int TotalCount { get; set; }
        public bool HasNext { get; set; }
        public static ApiResponse<T> SuccessResult(T data, string message = "İşlem başarılı", HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiResponse<T>
            {
                Result = true,
                Message = message,
                StatusCode = statusCode,
                Data = data
            };
        }

        public static ApiResponse<T> ErrorResult(string message = "İşlem başarısız", HttpStatusCode statusCode = HttpStatusCode.BadRequest, T data = default)
        {
            return new ApiResponse<T>
            {
                Result = false,
                ErrorMessage = message,
                StatusCode = statusCode,
                Data = data
            };
        }
    }
}