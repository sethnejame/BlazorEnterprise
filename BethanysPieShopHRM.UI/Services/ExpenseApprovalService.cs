using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using BethanysPieShopHRM.UI.Interfaces;

namespace BethanysPieShopHRM.UI.Services
{
    public class ExpenseApprovalService : IExpenseApprovalService
    {
        private readonly IEmployeeDataService _employeeDataService;

        public ExpenseApprovalService (IEmployeeDataService employeeDataService)
        {
            _employeeDataService = employeeDataService;
        }

        public async Task<ExpenseStatus> GetExpenseStatus(Expense expense)
        {
            var employee = await _employeeDataService.GetEmployeeDetails(expense.EmployeeId);

            if (employee.IsFTE)
            {
                switch (expense.ExpenseType)
                {
                    case ExpenseType.Conference:
                        return ExpenseStatus.Denied;
                    case ExpenseType.Travel:
                        return ExpenseStatus.Denied;
                    case ExpenseType.Hotel:
                        return ExpenseStatus.Denied;
                    case ExpenseType.Food:
                        return ExpenseStatus.Denied;
                }

            }
            else
            {
                if (expense.ExpenseType == ExpenseType.Food && expense.Amount > 250)
                {
                    return ExpenseStatus.Denied;
                }

                if (expense.Amount > 5000)
                {
                    return ExpenseStatus.Denied;
                }
            }

            if(employee.JobCategory?.JobCategoryName == "Sales" && expense.ExpenseType == ExpenseType.Training)
            {
                return ExpenseStatus.Denied;
            }

            if (!employee.IsOPEX)
            {
                switch(expense.ExpenseType)
                {
                    case ExpenseType.Conference:
                        return ExpenseStatus.Denied;
                    case ExpenseType.Training:
                        return ExpenseStatus.Denied;
                }
            }

            return ExpenseStatus.Pending;
        }
    }
}
