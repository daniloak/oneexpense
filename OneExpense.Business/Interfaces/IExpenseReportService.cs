using OneExpense.Business.Models;
using System;
using System.Threading.Tasks;

namespace OneExpense.Business.Interfaces
{
    public interface IExpenseReportService : IDisposable
    {
        Task<bool> Add(ExpenseReport expenseReport);
        Task<bool> Update(ExpenseReport expenseReport);
        Task<bool> Delete(Guid id);
    }
}
