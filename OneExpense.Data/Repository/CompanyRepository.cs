using OneExpense.Business.Interfaces;
using OneExpense.Business.Models;
using OneExpense.Data.Context;

namespace OneExpense.Data.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(OneExpenseDbContext context) : base(context) { }
    }
}
