using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Octokit;
using Octokit.Internal;
using Refit;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoronaApp.Api.Middleware
{
    public class ValedationMiddleware: ExceptionFilterAttribute
    {
        //private readonly RequestDelegate‏ requestDelegate;
        //public ValedationMiddleware(RequestDelegate‏ requestDelegate)
        //{
        //    this.requestDelegate = requestDelegate;
        //}
        private readonly RequestDelegate _next;

        public ValedationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public  async Task Invoke(HttpContext httpContext)
        {
            try
            {
                Log.Information("all good");
                await _next(httpContext);
            }
            catch (Exception e)
            {
                Log.Error("mistake 500");
                await HandleExceptionAsync(httpContext, e); ;
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            //if (ex is MyNotFoundException) code = HttpStatusCode.NotFound;
            //else if (ex is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            //else if (ex is MyException) code = HttpStatusCode.BadRequest;
            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }





    }
}
