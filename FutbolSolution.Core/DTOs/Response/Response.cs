using System.Collections.Generic;

namespace FutbolSolution.Core.DTOs.Response
{
    public class ResponseDTO<T>
    {
        public T Data { get; set; }
        public bool StatusCode { get; set; }
        public List<string> Errors { get; set; }
        public ErrorDto Error { get; private set; }
        public bool IsSuccessful { get; set; }

        public static ResponseDTO<T> Success(bool statusCode, T data)
        {
            return new ResponseDTO<T> { StatusCode = statusCode, Data = data, IsSuccessful = true };
        }

        public static ResponseDTO<T> Success(bool statusCode)
        {
            return new ResponseDTO<T> { StatusCode = statusCode, IsSuccessful = true };
        }

        public static ResponseDTO<T> Success(bool statusCode, string error)
        {
            return new ResponseDTO<T> { StatusCode = statusCode, Errors = new List<string> { error }, IsSuccessful = true };
        }

        public static ResponseDTO<T> Fail(ErrorDto errorDto, bool statusCode)
        {
            return new ResponseDTO<T>
            {
                Error = errorDto,
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }

        public static ResponseDTO<T> Fail(string errorMessage, bool statusCode, bool isShow)
        {
            var errorDto = new ErrorDto(errorMessage, isShow);
            return new ResponseDTO<T> { Error = errorDto, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
