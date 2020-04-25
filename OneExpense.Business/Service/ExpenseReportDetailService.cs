using OneExpense.Business.Interfaces;
using OneExpense.Business.Models;
using OneExpense.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OneExpense.Business.Service
{
    public class ExpenseReportDetailService : BaseService, IExpenseReportDetailService
    {
        private readonly IExpenseReportRepository _expenseReportRepository;
        private readonly IExpenseReportDetailRepository _expenseReportDetailRepository;

        public ExpenseReportDetailService(IExpenseReportRepository expenseRepository,
                                          IExpenseReportDetailRepository expenseDetailRepository,
                                          INotifier notifier) : base(notifier)
        {
            _expenseReportRepository = expenseRepository;
            _expenseReportDetailRepository = expenseDetailRepository;
        }

        public async Task Add(ExpenseReportDetail expenseReportDetail)
        {
            if (!Validate(new ExpenseReportDetailValidation(), expenseReportDetail)) return;

            await _expenseReportDetailRepository.Add(expenseReportDetail);
        }

        public async Task Update(ExpenseReportDetail expenseReportDetail)
        {
            if (!Validate(new ExpenseReportDetailValidation(), expenseReportDetail)) return;

            await _expenseReportDetailRepository.Update(expenseReportDetail);
        }

        public async Task Delete(Guid id)
        {
            await _expenseReportDetailRepository.Delete(id);
        }

        public void Dispose()
        {
            _expenseReportRepository?.Dispose();
        }
    }
}
