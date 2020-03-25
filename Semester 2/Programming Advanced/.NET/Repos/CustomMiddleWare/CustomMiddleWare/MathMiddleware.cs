using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CustomMiddleWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MathMiddleware
    {
        private readonly RequestDelegate _next;

        public MathMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/math", out var operatorPath))
            {
                StreamReader reader = new StreamReader(httpContext.Request.Body);
                string bodyText = await reader.ReadToEndAsync();

                string operatorName = operatorPath.ToString().Substring(1);
                string[] digits = bodyText.Split(new[] { "-", ";", "," }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                var result = httpContext.RequestServices.GetService<ICalculator>().ExecuteOperation(operatorName, digits);

                if (result == null)
                {
                    await _next(httpContext);
                }
                else
                {
                    await httpContext.Response.WriteAsync($"Result: [{result}]");
                }

            }
            else
            {
                await _next(httpContext);
            }
        }

        // Extension method used to add the middleware to the HTTP request pipeline.
        public static class MathMiddlewareExtensions
        {
            public static IApplicationBuilder UseMath(IApplicationBuilder builder)
            {
                return builder.UseMiddleware<MathMiddleware>();
            }
        }
    }
}
