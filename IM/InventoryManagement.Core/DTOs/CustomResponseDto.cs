﻿using System.Text.Json.Serialization;

namespace InventoryManagement.Core.DTOs
{
    public class CustomResponseDto<T>
    {

        public T Data { get; set; } = default!; //sonradan eklendi

        [JsonIgnore]
        public int StatusCode { get; set; }
        public List<String>? Errors { get; set; } = default!; //sonradan eklendi


        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Data = data, Errors = null };
        }

        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<String> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<String> { errors } };
        }


    }
}
