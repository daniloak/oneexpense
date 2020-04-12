using OneExpense.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneExpense.Business.Interfaces
{
    public interface IExpenseRepository : IRepository<Expense>
    {
    }
}
