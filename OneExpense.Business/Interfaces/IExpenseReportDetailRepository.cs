using OneExpense.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OneExpense.Business.Interfaces
{
    public interface IExpenseReportDetailRepository : IRepository<ExpenseReportDetail>
    {
        Task<IEnumerable<ExpenseReportDetail>> GetDetailsByExpenseId(Guid id);
    }
}
