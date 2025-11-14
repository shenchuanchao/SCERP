using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCERP.Domain.DTOs
{
    /// <summary>
    /// 通用API响应类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string? ErrorCode { get; set; }
        /// <summary>
        /// 成功响应
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponse<T> Success(T data) => new()
        {
            IsSuccess = true,
            Data = data,
            StatusCode = 200
        };
        /// <summary>
        /// 失败响应
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="statusCode"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static ApiResponse<T> Failure(string errorMessage, int statusCode, string? errorCode = null) => new()
        {
            IsSuccess = false,
            ErrorMessage = errorMessage,
            StatusCode = statusCode,
            ErrorCode = errorCode
        };
    }
    /// <summary>
    /// 通用API响应类
    /// </summary>
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string? ErrorCode { get; set; }
        /// <summary>
        /// 成功响应
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResponse Success(string message) => new()
        {
            IsSuccess = true,
            StatusCode = 200,
            Message = message
        };
        /// <summary>
        /// 失败响应
        /// </summary>
        /// <param name="message"></param>
        /// <param name="statusCode"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static ApiResponse Failure(string message,int statusCode, string? errorCode = null) => new()
        {
            IsSuccess = false,
            StatusCode = statusCode,
            ErrorCode = errorCode,
            Message = message,
        };

    }

}
