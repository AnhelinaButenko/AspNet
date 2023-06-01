using AspNet_Configuration;
using Google.Api.Ads.Common.Lib;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace AspNet_Configuration_task1
{
    public interface IEmployeeService
    {
        string GetCompanyName(List<Company> companies);
    }

    public class EmployeeService : IEmployeeService
    {
        public string GetCompanyName(List<Company> companies)
        {
            var res = companies.OrderBy(company => company.Employees).LastOrDefault();

            if (res != null)
            {
                return res.Name;
            }

            return string.Empty;
        }
    }
}
