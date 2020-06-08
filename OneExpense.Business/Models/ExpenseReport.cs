using System;
using System.Collections.Generic;

namespace OneExpense.Business.Models
{
    public class ExpenseReport : Entity
    {
        public Guid UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<ExpenseReportDetail> Details { get; set; }
    }
}
