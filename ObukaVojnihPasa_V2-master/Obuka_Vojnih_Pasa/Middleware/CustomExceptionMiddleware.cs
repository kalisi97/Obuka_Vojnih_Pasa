using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Obuka_Vojnih_Pasa.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Obuka_Vojnih_Pasa.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;
        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> loggerFactory)
        {
            _next = next;
            _logger = loggerFactory;
            _logger.LogDebug("NLog injected into CustomExceptionMiddleware");


        }

        public async Task Invoke(HttpContext context)
        {

            try
            {
                await _next.Invoke(context);
                switch (context.Response.StatusCode)
                {
                    case (int)HttpStatusCode.NotFound:

                        HandlePageNotFound(context); break;
                    case (int)HttpStatusCode.InternalServerError: HandleInternalServerError(context); break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                HandleException(context, ex);
            }

        }

        private void HandleInternalServerError(HttpContext context)
        {
            StringBuilder sb = new StringBuilder($"An error has occurred on {context.Request.Host}. \r\n \r\n");
            sb.Append($"Path = {context.Request.Path} \r\n \r\n");
            _logger.LogWarning($"500 error occured. Path = " +
           sb);
            context.Response.Redirect("/Home/ServerError");
        }

        private void HandlePageNotFound(HttpContext context)
        {
            StringBuilder sb = new StringBuilder($"An error has occurred on {context.Request.Host}. \r\n \r\n");
            sb.Append($"Path = {context.Request.Path} \r\n \r\n");
            _logger.LogWarning(
           sb.ToString());

            string pageNotFound = context.Request.Host.ToString() + context.Request.Path.ToString();
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddSeconds(20);
            cookieOptions.IsEssential = true;

            context.Response.Cookies.Append("PageNotFound", pageNotFound, cookieOptions);
            context.Response.Redirect("/PageNotFound");
        }

        private void HandleException(HttpContext context, Exception ex)
        {

            StringBuilder sb = new StringBuilder($"An error has occurred on {context.Request.Host}. \r\n \r\n");
            sb.Append($"Path = {context.Request.Path} \r\n \r\n");
            sb.Append($"Error Message = {ex.Message} \r\n");
            sb.Append($"Error Source = {ex.Source} \r\n");

            if (ex.InnerException != null)
                sb.Append($"Inner Exception = {ex.InnerException.ToString()} \r\n");
            else
                sb.Append("Inner Exception = null \r\n");

            sb.Append($"Error StackTrace = {ex.StackTrace} \r\n");
            _logger.LogError(sb.ToString());

            context.Response.Redirect("/Error");
        }


    }
    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}

