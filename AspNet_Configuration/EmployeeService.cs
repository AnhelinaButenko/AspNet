namespace AspNet_Configuration_task1
{
    public interface IEmployeeService
    {
       string GetCompanyWithMoreEmployees(string companyName, int employeers);
    }

    public class EmployeeService : IEmployeeService
    {
        public string GetCompanyWithMoreEmployees(string companyName, int employeers)
        {
           
            return "";
        }
    }
}
