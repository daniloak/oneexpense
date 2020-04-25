using OneExpense.Business.Models;
using System;
using System.Threading.Tasks;

namespace OneExpense.Business.Interfaces
{
    public interface IExpenseReportDetailService : IDisposable
    {
        Task Add(ExpenseReportDetail expenseReportDetail);
        Task Update(ExpenseReportDetail expenseReportDetail);
        Task Delete(Guid id);
    }
}
