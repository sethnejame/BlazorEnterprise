using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using BethanysPieShopHRM.UI.Interfaces;

namespace BethanysPieShopHRM.UI.Services
{
    public class ManagerApprovalService : IExpenseApprovalService
    {
        private readonly IEmployeeDataService _employeeDataService;
        public ManagerApprovalService(IEmployeeDataService employeeDataService)
        {
            _employeeDataService = employeeDataService;
        }

        public async Task<ExpenseStatus> GetExpenseStatus(Expense expense)
        {
            var employee = await _employeeDataService.GetEmployeeDetails(expense.EmployeeId);

            // New sample approval scenarios
            if (employee.IsFTE)
            {
                if (expense.Amount < 250)
                {
                    switch (expense.ExpenseType)
                    {
                        case ExpenseType.Training:
                            return ExpenseStatus.Approved;
                        case ExpenseType.Food:
                            return ExpenseStatus.Approved;
                        case ExpenseType.Office:
                            return ExpenseStatus.Approved;
                    }
                }


                if (employee.JobCategory?.JobCategoryName == "Sales" && expense.Amount < 250)
                {
                    switch (expense.ExpenseType)
                    {
                        case ExpenseType.Transportation:
                            return ExpenseStatus.Approved;
                        case ExpenseType.Travel:
                            return ExpenseStatus.Approved;
                        case ExpenseType.Hotel:
                            return ExpenseStatus.Approved;
                    }
                }
            }
            return ExpenseStatus.Pending;
        }
    }
}
