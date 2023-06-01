using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.Ini;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Xml;

namespace AspNet_Configuration
{
    public class Startup
    {
        public IConfiguration AppConfig { get; set; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(env.ContentRootPath);

            builder.AddIniFile("IniConfig.ini");
            builder.AddJsonFile("JsonConfig.json");
            builder.AddXmlFile("XmlConfig.xml");

            AppConfig = builder.Build();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) => 
            {
                var nameFirstCompanyIni = AppConfig["NameFirstCompanyIni"];
                var numberOfEmployeesFirstCompanyIni = AppConfig["NumberOfEmployeesFirstCompanyIni"];

                var nameSecondCompanyIni = AppConfig["NameSecondCompanyIni"];
                var numberOfEmployeesSecondCompanyIni = AppConfig["NumberOfEmployeesSecondCompanyIni"];

                var nameThirdCompanyIni = AppConfig["NameThirdCompanyIni"];
                var numberOfEmployeesThirdCompanyIni = AppConfig["NumberOfEmployeesThirdCompanyIni"];

                await context.Response.WriteAsync($"First company Ini: {nameFirstCompanyIni} - employeers: {numberOfEmployeesFirstCompanyIni}, " +
                    $"Second company ini: {nameSecondCompanyIni} - employeers: {numberOfEmployeesSecondCompanyIni}, " +
                    $"Third company ini: {nameThirdCompanyIni} - employeers: {numberOfEmployeesThirdCompanyIni} ");

                var section1 = AppConfig.GetValue<int>("NumberOfEmployeesFirstCompanyIni");
                var section2 = AppConfig.GetValue<int>("NumberOfEmployeesSecondCompanyIni");
                var section3 = AppConfig.GetValue<int>("NumberOfEmployeesThirdCompanyIni");

                if (section1 > section2 && section1 > section3)
                {
                    await context.Response.WriteAsync($" More employeers Ini: {nameFirstCompanyIni}");
                }
                if (section2 > section3 && section2 > section1)
                {
                    await context.Response.WriteAsync($" More employeers Ini: {nameSecondCompanyIni} ");
                }
                else
                {
                    await context.Response.WriteAsync($" More employeers Ini: {nameThirdCompanyIni} ");
                }


                var nameFirstCompanyJson = AppConfig["NameFirstCompanyJson"];
                var numberOfEmployeesFirstCompanyJson = AppConfig["NumberOfEmployeesFirstCompanyJson"];
               
                var nameSecondCompanyJson = AppConfig["NameSecondCompanyJson"];
                var numberOfEmployeesSecondCompanyJson = AppConfig["NumberOfEmployeesSecondCompanyJson"];

                var nameThirdCompanyJson = AppConfig["NameThirdCompanyJson"];
                var numberOfEmployeesThirdCompanyJson = AppConfig["NumberOfEmployeesThirdCompanyJson"];

                await context.Response.WriteAsync($"First company Json: {nameFirstCompanyJson} - employeers: {numberOfEmployeesFirstCompanyJson}, " +
                    $"Second company json: {nameSecondCompanyJson} - employeers: {numberOfEmployeesSecondCompanyJson}, " +
                    $"Third company json: {nameThirdCompanyJson} - employeers: {numberOfEmployeesThirdCompanyJson} ");


                var nameFirstCompanyXml = AppConfig["NameFirstCompanyXml"];
                var numberOfEmployeesFirstCompanyXml = AppConfig["NumberOfEmployeesFirstCompanyXml"];

                var nameSecondCompanyXml = AppConfig["NameSecondCompanyXml"];
                var numberOfEmployeesSecondCompanyXml = AppConfig["NumberOfEmployeesSecondCompanyXml"];

                var nameThirdCompanyXml = AppConfig["NameFirstCompanyXml"];
                var numberOfEmployeesThirdCompanyXml = AppConfig["NumberOfEmployeesFirstCompanyXml"];

                await context.Response.WriteAsync($"First company Xml: {nameFirstCompanyXml} - employeers: {numberOfEmployeesFirstCompanyXml}, " +
                    $"Second company json: {nameSecondCompanyXml} - employeers: {numberOfEmployeesSecondCompanyXml}, " +
                    $"Third company json: {nameThirdCompanyXml} - employeers: {numberOfEmployeesThirdCompanyXml} ");
            });
        }
    }
}
