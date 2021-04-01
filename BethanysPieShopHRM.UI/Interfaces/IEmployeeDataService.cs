using System.Collections.Generic;
using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;

namespace BethanysPieShopHRM.UI.Services
{
    public interface IEmployeeDataService
    {
        public Employee SavedEmployee { get; set; }
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeDetails(int employeeId);
        Task<Employee> AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(int employeeId);
    }
}
