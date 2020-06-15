using OneExpense.Business.Models;
using System;
using System.Threading.Tasks;

namespace OneExpense.Business.Interfaces
{
    public interface IExpenseReportDetailService : IDisposable
    {
        Task<bool> Add(ExpenseReportDetail expenseReportDetail);
        Task<bool> Update(ExpenseReportDetail expenseReportDetail);
        Task<bool> Delete(Guid id);
    }
}
