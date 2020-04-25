using Microsoft.EntityFrameworkCore;
using OneExpense.Business.Interfaces;
using OneExpense.Business.Models;
using OneExpense.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneExpense.Data.Repository
{
    public class ExpenseReportRepository : Repository<ExpenseReport>, IExpenseReportRepository
    {
        public ExpenseReportRepository(OneExpenseDbContext context) : base(context) { }

        public async Task<IEnumerable<ExpenseReport>> GetCompleteExpenseReport()
        {
            return await Db.ExpenseReports.AsNoTracking()
                .Include(c => c.Details)
                .ToListAsync();
        }

        public async Task<ExpenseReport> GetCompleteExpenseReportById(Guid id)
        {
            return await Db.ExpenseReports.AsNoTracking()
                .Include(c => c.Details)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
