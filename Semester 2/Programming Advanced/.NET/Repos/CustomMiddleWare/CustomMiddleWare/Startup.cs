using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CustomMiddleWare
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseFileServer();

            app.UseRouting();
            app.Use(next =>
            {
                return async context =>
                {
                    logger.LogInformation("Request incoming");
                if (context.Request.Path.StartsWithSegments("/math",out var operatorPath))
                    {
                        
                        logger.LogInformation("Request handled");
                    }
                    else
                    {
                        await next(context);
                        logger.LogInformation("Request outgoing to next middleware");
                    }
                };
            });


            app.UseWelcomePage(new WelcomePageOptions
            {
                Path = "/welcome"
            });

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
                //endpoints.MapControllerRoute("Default","{Controller}/{Action}");
                endpoints.MapDefaultControllerRoute();

            });

            app.Run(async (context) =>
            {
                throw new Exception();
            });

        }


        public HttpContext Operation(PathString operation)
        {
            switch (operation)
            {
                case "Add":

                    break;
                case  "Divide":

                    break;
                case "Multiply":

                    break;
                case "Subtract":

                    break;
                case "Faculty":

                    break;
            }
            
        }


    }
}
