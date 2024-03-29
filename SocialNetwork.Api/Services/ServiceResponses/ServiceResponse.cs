﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Services.ServiceResponses
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public ServiceResponse(T responseData, bool success, string message)
        {
            Message = message;
            Success = success;
            Data = responseData;
        }

        public static ServiceResponse<bool> Ok(bool data = true, string message = "") => new ServiceResponse<bool>(data, true, message);

        public static ServiceResponse<T> Ok(T data, string message = "")
        {
            return new ServiceResponse<T>(data, true, message);
        }

        public static ServiceResponse<T> Error(string message = "")
        {
            return new ServiceResponse<T>(default(T), false, message);
        }
    }
}
