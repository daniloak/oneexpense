using OneExpense.Business.Models;
using System;
using System.Threading.Tasks;

namespace OneExpense.Business.Interfaces
{
    public interface IExpenseReportService : IDisposable
    {
        Task Add(ExpenseReport expenseReport);
        Task Update(ExpenseReport expenseReport);
        Task Delete(Guid id);
    }
}
