using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Routing_task4
{
    public class Startup
    {
        public IConfiguration AppConfig { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var routeBuilder = new RouteBuilder(app);

            var builder = new ConfigurationBuilder();

            builder.SetBasePath(env.ContentRootPath);

            builder.AddJsonFile("JsonConfigBooks.json");
            builder.AddJsonFile("JsonConfigProfiles.json");

            AppConfig = builder.Build();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            routeBuilder.MapRoute("Library", HandleLibrary);

            routeBuilder.MapRoute("Library/Books", HandleLibraryBooks);

            routeBuilder.MapRoute("Library/Profile/{Id?}", HandleLibraryProfile);

            app.UseRouter(routeBuilder.Build());

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Default page.");
            });
        }

        private async Task HandleLibrary(HttpContext context)
        {
            await context.Response.WriteAsync("Welcome to the library!");
        }

        private async Task HandleLibraryBooks(HttpContext context)
        {
            var myArray = AppConfig.GetSection("Books").Get<Book[]>();

            string json = JsonConvert.SerializeObject(myArray);

            await context.Response.WriteAsync(json);
        }

        private async Task HandleLibraryProfile(HttpContext context)
        {
            var id = context.GetRouteValue("id");

            bool successNum = int.TryParse((string)id, out int selectedNumberForId);

            if (successNum)
            {
                if (selectedNumberForId == 0 || selectedNumberForId <= 5)
                {
                    await Method(context, selectedNumberForId);
                }
                else
                {
                    await Method(context);
                }
            }
            await Method(context);
        }

        private async Task Method(HttpContext context, int? id = null)
        {
            var array = AppConfig.GetSection("Profiles").Get<Profile[]>();

            var myArray = array.Where(a => a.Id == id).ToList();

            string json = JsonConvert.SerializeObject(myArray);

            await context.Response.WriteAsync(json);
        }
    }
}
