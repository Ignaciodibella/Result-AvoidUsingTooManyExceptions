using NightOpsBackEnd.DTOs.ErrorResponse;
using System.Net;

namespace NightOpsBackEnd.ErrorHandler
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Data { get; }
        public ErrorResponseDTO Error { get; }

        private static Dictionary<int, string> httpStatusCodes = new Dictionary<int, string>
        {
            { 100, "Continue" },
            { 101, "Switching Protocols" },
            { 200, "OK" },
            { 201, "Created" },
            { 202, "Accepted" },
            { 203, "Non-Authoritative Information" },
            { 204, "No Content" },
            { 205, "Reset Content" },
            { 206, "Partial Content" },
            { 300, "Multiple Choices" },
            { 301, "Moved Permanently" },
            { 302, "Found" },
            { 303, "See Other" },
            { 304, "Not Modified" },
            { 305, "Use Proxy" },
            { 307, "Temporary Redirect" },
            { 400, "Bad Request" },
            { 401, "Unauthorized" },
            { 402, "Payment Required" },
            { 403, "Forbidden" },
            { 404, "Not Found" },
            { 405, "Method Not Allowed" },
            { 406, "Not Acceptable" },
            { 407, "Proxy Authentication Required" },
            { 408, "Request Timeout" },
            { 409, "Conflict" },
            { 410, "Gone" },
            { 411, "Length Required" },
            { 412, "Precondition Failed" },
            { 413, "Payload Too Large" },
            { 414, "URI Too Long" },
            { 415, "Unsupported Media Type" },
            { 416, "Range Not Satisfiable" },
            { 417, "Expectation Failed" },
            { 500, "Internal Server Error" },
            { 501, "Not Implemented" },
            { 502, "Bad Gateway" },
            { 503, "Service Unavailable" },
            { 504, "Gateway Timeout" },
            { 505, "HTTP Version Not Supported" }
        };

        private Result(bool isSuccess, T result, ErrorResponseDTO error)
        {
            IsSuccess = isSuccess;
            Data = result;
            Error = error;
        }

        public static Result<T> Success(T result)
        {
            return new Result<T>(true, result, null);
        }

        public static Result<T> Failure(ErrorResponseDTO error)
        {
            return new Result<T>(false, default, error);
        }

        public static Result<T> Unauthorized()
        {
            int statusCode = 401;
            var unauthorizedResponse = new ErrorResponseDTO
            {
                Status = statusCode,
                Error = GetHttpStatusName(statusCode),
                Message = "El usuario no se encuentra autenticado"
            };

            return new Result<T>(false, default, unauthorizedResponse);
        }
        public static Result<T> ErrorSender(int status, string message)
        {
            return Result<T>.Failure(new ErrorResponseDTO
            {
                Status = status,
                Error = GetHttpStatusName(status),
                Message = message
            });
        }

        private static string GetHttpStatusName(int statusCode)
        {
            if (httpStatusCodes.ContainsKey(statusCode))
            {
                return httpStatusCodes[statusCode];
            }
            return "Error Desconocido";
        }
    }
}
