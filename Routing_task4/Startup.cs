using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Routing_task4
{
    public class Startup
    {
        //+ Задать следующие пути маршрутизации: Library, Library\Books, Library\Profile.
        //+ Запрос, отправленный по адресу Library, должен возвращать текст приветствия.
        //+ Путь Library\Books должен выводить список книг, записанный в виде файла конфигурации любого типа на выбор учащегося.       
        //+ Путь Library\Profile должен принимать в качестве необязательного параметра id,где,в соответствии с введенным значением
        //(маршрут должен принимать только целочисленные значения от 0 до 5)
        //будет в экран браузера выведена информация о пользователе библиотеки под определенным id(информация должна быть
        //записана в виде файла конфигурации любого формата).
        //В случае, если пользователь не ввел необязательный параметр, должна выводится информация о самом пользователe

        public IConfiguration AppConfig { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(env.ContentRootPath);

            builder.AddJsonFile("JsonConfigBooks.json");
            builder.AddJsonFile("JsonConfigProfiles.json");

            AppConfig = builder.Build();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var myRouteHandlerLibrary = new RouteHandler(HandleLibrary);
            var myRouteHandlerLibraryBooks = new RouteHandler(HandleLibraryBooks);
            var myRouteHandlerLibraryProfile = new RouteHandler(HandleLibraryProfile);

            var routeBuilderLibrary = new RouteBuilder(app, myRouteHandlerLibrary);
            var routeBuilderLibraryBooks = new RouteBuilder(app, myRouteHandlerLibraryBooks);
            var routeBuilderLibraryProfile = new RouteBuilder(app, myRouteHandlerLibraryProfile);

            routeBuilderLibrary.MapRoute("default", "Library");
            routeBuilderLibraryBooks.MapRoute("default", "Library/Books");
            routeBuilderLibraryProfile.MapRoute("default", "Library/Profile/{Id?}");

            app.UseRouter(routeBuilderLibrary.Build());
            app.UseRouter(routeBuilderLibraryBooks.Build());
            app.UseRouter(routeBuilderLibraryProfile.Build());

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
            if (context.Request.Method == "GET")
            {
                var id = context.GetRouteValue("id");

                int selectedNumberForId;

                bool successNum = int.TryParse((string)id, out selectedNumberForId);

                if (successNum == true)
                {
                    selectedNumberForId = Convert.ToInt32(id);

                    if (selectedNumberForId == 0 || selectedNumberForId <= 5)
                    {
                        var array = AppConfig.GetSection("Profiles").Get<Profile[]>();

                        var myArray = array.Where(a => a.Id == selectedNumberForId).ToList();

                        string json = JsonConvert.SerializeObject(myArray);

                        await context.Response.WriteAsync(json);
                    }
                    else
                    {
                        var arrayProfil1 = AppConfig.GetSection("Profiles").Get<Profile[]>();

                        var myArrayProfil1 = arrayProfil1.LastOrDefault();

                        string jsonProfil1 = JsonConvert.SerializeObject(myArrayProfil1);

                        await context.Response.WriteAsync(jsonProfil1);
                    }
                }           
            }

            var arrayProfil = AppConfig.GetSection("Profiles").Get<Profile[]>();

            var myArrayProfil = arrayProfil.LastOrDefault();

            string jsonProfil = JsonConvert.SerializeObject(myArrayProfil);

            await context.Response.WriteAsync(jsonProfil);
        }
    }  
}
