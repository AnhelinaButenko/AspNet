using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Routing_task2
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

            routeBuilder.MapRoute("{controller}/{action}/{id}/{*catchall}",
  
                async context => {

                    var contoller = context.GetRouteValue("controller");
                    await IsNumber(contoller, context);

                    var action = context.GetRouteValue("action");
                    await IsNumber(action, context);

                    var id = context.GetRouteValue("id");
                    await IsNumber(id, context);
                });

            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Default page.");
            });
        }

        public async Task IsNumber (object segment, HttpContext context)
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
