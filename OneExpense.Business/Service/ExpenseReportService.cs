using OneExpense.Business.Interfaces;
using OneExpense.Business.Models;
using OneExpense.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OneExpense.Business.Service
{
    public class ExpenseReportService : BaseService, IExpenseReportService
    {
        private readonly IExpenseReportRepository _expenseReportRepository;
        private readonly IExpenseReportDetailRepository _expenseReportDetailRepository;

        public ExpenseReportService(IExpenseReportRepository expenseRepository,
                                    IExpenseReportDetailRepository expenseDetailRepository,
                                    INotifier notifier) : base(notifier)
        {
            _expenseReportRepository = expenseRepository;
            _expenseReportDetailRepository = expenseDetailRepository;
        }

        public async Task Add(ExpenseReport expenseReport)
        {
            expenseReport.Total = expenseReport.Details.Sum(p => p.Amount);

            if (!Validate(new ExpenseReportValidation(), expenseReport)) return;

            await _expenseReportRepository.Add(expenseReport);
        }

        public async Task Update(ExpenseReport expenseReport)
        {
            expenseReport.Total = expenseReport.Details.Sum(p => p.Amount);

            if (!Validate(new ExpenseReportValidation(), expenseReport)) return;

            await _expenseReportRepository.Update(expenseReport);
        }

        public async Task Delete(Guid id)
        {
            var expenseDetails = await _expenseReportDetailRepository.GetDetailsByExpenseId(id);

            foreach(var expenseDetail in expenseDetails)
            {
                await _expenseReportDetailRepository.Delete(expenseDetail.Id);
            }

            await _expenseReportRepository.Delete(id);
        }

        public void Dispose()
        {
            _expenseReportRepository?.Dispose();
        }
    }
}
