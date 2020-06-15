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

        public async Task<bool> Add(ExpenseReport expenseReport)
        {
            expenseReport.Total = expenseReport.Details.Sum(p => p.Amount);

            if (!Validate(new ExpenseReportValidation(), expenseReport)) return false;

            _expenseReportRepository.Add(expenseReport);

            return await _expenseReportRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Update(ExpenseReport expenseReport)
        {
            expenseReport.Total = expenseReport.Details.Sum(p => p.Amount);

            if (!Validate(new ExpenseReportValidation(), expenseReport)) return false;

            _expenseReportRepository.Update(expenseReport);

            return await _expenseReportRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Delete(Guid id)
        {
            var expenseDetails = await _expenseReportDetailRepository.GetDetailsByExpenseId(id);

            foreach(var expenseDetail in expenseDetails)
            {
                _expenseReportDetailRepository.Delete(expenseDetail.Id);
            }

            _expenseReportRepository.Delete(id);

            return await _expenseReportRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _expenseReportRepository?.Dispose();
        }
    }
}
