using OneExpense.Business.Interfaces;
using OneExpense.Business.Models;
using OneExpense.Data.Context;

namespace OneExpense.Data.Repository
{
    public class ExpenseRepository : Repository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(OneExpenseDbContext context) : base(context) { }
    }
}
