using OneExpense.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneExpense.Business.Interfaces
{
    public interface IExpenseService : IDisposable
    {
        Task Add(Expense expense);
        Task Update(Expense expense);
        Task Delete(Guid id);
    }
}
