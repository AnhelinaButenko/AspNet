using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using AspNet_Configuration_task1;
using Newtonsoft.Json;
using System.Xml;

namespace AspNet_Configuration
{
    public class Startup
    {
        public IConfiguration AppConfig { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IEmployeeService employeeService)
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

            app.Run((RequestDelegate)(async (context) => 
            {
                Company firstCompanyIni = new Company()
                {
                    Name = AppConfig["NameFirstCompanyIni"],
                    Employees = AppConfig.GetValue<int>("NumberOfEmployeesFirstCompanyIni")
                };
                Company secondCompanyIni = new Company()
                {
                    Name = AppConfig["NameSecondCompanyIni"],
                    Employees = AppConfig.GetValue<int>("NumberOfEmployeesSecondCompanyIni")
                };
                Company thirdCompanyIni = new Company()
                {
                    Name = AppConfig["NameThirdCompanyIni"],
                    Employees = AppConfig.GetValue<int>("NumberOfEmployeesThirdCompanyIni")
                };

                List<Company> listCompaniesIni = new List<Company>() { firstCompanyIni, secondCompanyIni, thirdCompanyIni };

                var companyNameIni = employeeService.GetCompanyName(listCompaniesIni);

                await context.Response.WriteAsync($" More employeers Ini: {companyNameIni}");

                string json3 = JsonConvert.SerializeObject(listCompaniesIni);

                await context.Response.WriteAsync(json3);

              
                Company firstCompanyJson = new Company()
                {
                    Name = AppConfig["NameFirstCompanyJson"],
                    Employees = AppConfig.GetValue<int>("NumberOfEmployeesFirstCompanyJson")
                };
                Company secondCompanyJson = new Company()
                {
                    Name = AppConfig["NameSecondCompanyJson"],
                    Employees = AppConfig.GetValue<int>("NumberOfEmployeesSecondCompanyJson")
                };
                Company thirdCompanyJson = new Company()
                {
                    Name = AppConfig["NameThirdCompanyJson"],
                    Employees = AppConfig.GetValue<int>("NumberOfEmployeesThirdCompanyJson")
                };

                List<Company> listCompaniesJson = new List<Company>() { firstCompanyJson, secondCompanyJson, thirdCompanyJson };

                string json = JsonConvert.SerializeObject(listCompaniesJson);

                var companyNameJson = employeeService.GetCompanyName(listCompaniesJson);

                await context.Response.WriteAsync($" More employeers Json: {companyNameJson}");

                await context.Response.WriteAsync(json);

                
                Company firstCompanyXml = new Company()
                {
                    Name = AppConfig["NameFirstCompanyXml"],
                    Employees = AppConfig.GetValue<int>("NumberOfEmployeesFirstCompanyXml")
                };
                Company secondCompanyXml = new Company()
                {
                    Name = AppConfig["NameSecondCompanyXml"],
                    Employees = AppConfig.GetValue<int>("NumberOfEmployeesSecondCompanyXml")
                };
                Company thirdCompanyXml = new Company()
                {
                    Name = AppConfig["NameThirdCompanyXml"],
                    Employees = AppConfig.GetValue<int>("NumberOfEmployeesThirdCompanyXml")
                };

                List<Company> listCompaniesXml = new List<Company>() { firstCompanyXml, secondCompanyXml, thirdCompanyXml };

                string json2 = JsonConvert.SerializeObject(listCompaniesXml);

                var companyNameXml = employeeService.GetCompanyName(listCompaniesXml);

                await context.Response.WriteAsync($" More employeers Xml: {companyNameXml}");

                await context.Response.WriteAsync(json2);
            }));
        }
    }

    public class Company
    {
        public string Name { get; set; }

        public int Employees { get; set; }
    }
}
