using System;
using System.Collections.Generic;
using System.Text;

namespace OneExpense.Business.Models
{
    public class ExpenseDetails : Entity
    {
        public Guid ExpenseId { get; set; }
        public string Supplier { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Expense Expense { get; set; }
    }
}
