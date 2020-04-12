using System;

namespace OneExpense.API.ViewModel
{
    public class ExpenseViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
