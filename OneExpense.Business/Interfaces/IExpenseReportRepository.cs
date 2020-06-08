using OneExpense.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneExpense.Business.Interfaces
{
    public interface IExpenseReportRepository : IRepository<ExpenseReport>
    {
        Task<IEnumerable<ExpenseReport>> GetCompleteExpenseReport(Guid userId);
        Task<ExpenseReport> GetCompleteExpenseReportById(Guid id, Guid userId);
    }
}
