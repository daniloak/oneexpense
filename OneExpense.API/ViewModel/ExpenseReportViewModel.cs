using System;
using System.Collections.Generic;

namespace OneExpense.API.ViewModel
{
    public class ExpenseReportViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<ExpenseReportDetailViewModel> Details { get; set; }
    }
}
