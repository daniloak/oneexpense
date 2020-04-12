using System;
using System.Collections.Generic;

namespace OneExpense.Business.Models
{
    public class Expense : Entity
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public IEnumerable<ExpenseDetails> Details { get; set; }
    }
}
