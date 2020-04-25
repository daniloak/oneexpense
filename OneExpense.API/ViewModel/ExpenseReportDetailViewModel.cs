using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneExpense.API.ViewModel
{
    public class ExpenseReportDetailViewModel
    {
        public Guid Id { get; set; }
        public Guid ExpenseId { get; set; }
        public string Supplier { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Image { get; set; }
        public string ImageUpload { get; set; }
    }
}
