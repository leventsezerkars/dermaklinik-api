namespace DermaKlinik.API.Core.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public int StatusCode { get; set; }

        public static ApiResponse<T> SuccessResult(T data, string? message = null, int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                StatusCode = statusCode
            };
        }

        public static ApiResponse<T> ErrorResult(string error, int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = error,
                Errors = new List<string> { error },
                StatusCode = statusCode
            };
        }

        public static ApiResponse<T> ErrorResult(List<string> errors, int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = "İşlem başarısız",
                Errors = errors,
                StatusCode = statusCode
            };
        }
    }
} 