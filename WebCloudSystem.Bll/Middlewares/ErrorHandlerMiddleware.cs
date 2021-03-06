using WebCloudSystem.Bll.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;



namespace WebCloudSystem.Bll.Middlewares {
    public class ErrorHandlerMiddleware {
        private readonly RequestDelegate next;
        public ErrorHandlerMiddleware (RequestDelegate next) {
            this.next = next;
        }

        public async Task Invoke (HttpContext context /* other dependencies */ ) {
            try {
                await next (context);
            } catch (Exception ex) {
                await HandleExceptionAsync (context, ex);
            }
        }

        private static Task HandleExceptionAsync (HttpContext context, Exception exception) {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception is ResourceNotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is UnauthorizedException) code = HttpStatusCode.Unauthorized;
            else if (exception is BadRequestException) code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject (new { message = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync (result);
        }
    }
}