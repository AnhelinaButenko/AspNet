using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routing_task3
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var routeBuilder = new RouteBuilder(app);

            routeBuilder.MapRoute("{controller}",

                async context => {

                    if (context.Request.Method == "GET")
                    {
                        var contoller = context.GetRouteValue("controller");
                        await IsNumber(contoller, context);

                        var action = context.GetRouteValue("action");
                        await IsNumber(action, context);

                        var id = context.GetRouteValue("id");
                        await IsNumber(id, context);
                    }
                });

            routeBuilder.MapRoute("{controller}/{action}",

                async context => {

                    if (context.Request.Method == "GET")
                    {
                        var contoller = context.GetRouteValue("controller");
                        await IsNumber(contoller, context);

                        var action = context.GetRouteValue("action");
                        await IsNumber(action, context);

                        var id = context.GetRouteValue("id");
                        await IsNumber(id, context);
                    }
                });

            routeBuilder.MapRoute("{controller}/{action}/{id}",

                async context => {

                    if (context.Request.Method == "GET")
                    {
                        var contoller = context.GetRouteValue("controller");
                        await IsNumber(contoller, context);

                        var action = context.GetRouteValue("action");
                        await IsNumber(action, context);

                        var id = context.GetRouteValue("id");
                        await IsNumber(id, context);
                    }
                });

            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Default page.");
            });
        }

        public async Task IsNumber(object segment, HttpContext context)
        {
            int number;

            bool successNum = int.TryParse((string)segment, out number);
            if (successNum == true)
            {
                Convert.ToInt32(segment);
                await context.Response.WriteAsync($"<br>{segment}<br>");
            }
            else
            {
                await context.Response.WriteAsync("Mistake!");
            }
        }
    }
}
