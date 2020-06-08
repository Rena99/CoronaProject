using Jose;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaApp.Api.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //public async Task Invoke(HttpRequest req)
        //{
        //    try
        //    {
        //        if (req.Headers.authorization && req.Headers.authorization.split(' ')[0] == 'Token')
        //        {
        //            return req.Headers.authorization.split(' ')[1];
        //        }

        //        await _next(req);
        //    }
        //    catch (Exception e)
        //    {
        //        await HandleExceptionAsync(req, e); ;
        //    }
        //}

        //private Task HandleExceptionAsync(HttpRequest req, Exception e)
        //{
        //    return new ErrorContext('Invalid request!');
        //}
    }
}
