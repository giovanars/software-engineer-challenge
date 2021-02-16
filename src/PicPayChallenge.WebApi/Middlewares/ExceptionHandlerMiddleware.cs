using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PicPayChallenge.Core.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PicPayChallenge.WebApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exeption)
            {
                await HandleExceptionAsync(context, exeption);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exeption)
        {
            string payload;
            HttpStatusCode statusCode;

            switch (exeption)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    payload = JsonConvert.SerializeObject(new { error = new { message = validationException.Message, status = "400" } });
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    payload = JsonConvert.SerializeObject(new { error = new { message = "", status = "500"} });
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(payload);
        }
    }
}
