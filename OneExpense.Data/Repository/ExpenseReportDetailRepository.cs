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
    public class ExpenseReportDetailRepository : Repository<ExpenseReportDetail>, IExpenseReportDetailRepository
    {
        public ExpenseReportDetailRepository(OneExpenseDbContext context) : base(context) { }

        public async Task<IEnumerable<ExpenseReportDetail>> GetDetailsByExpenseId(Guid id)
        {
            return await Db.ExpenseReportDetails.AsNoTracking().Where(p => p.ExpenseId == id).ToListAsync();
        }
    }
}
