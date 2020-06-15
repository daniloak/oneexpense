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

        public async Task<bool> Add(ExpenseReportDetail expenseReportDetail)
        {
            if (!Validate(new ExpenseReportDetailValidation(), expenseReportDetail)) return false;

            _expenseReportDetailRepository.Add(expenseReportDetail);

            var expense = await _expenseReportRepository.GetById(expenseReportDetail.ExpenseId);
            expense.Total += expenseReportDetail.Amount;

            _expenseReportRepository.Update(expense);

            return await _expenseReportRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Update(ExpenseReportDetail expenseReportDetail)
        {
            if (!Validate(new ExpenseReportDetailValidation(), expenseReportDetail)) return false;

            _expenseReportDetailRepository.Update(expenseReportDetail);

            var expense = await _expenseReportRepository.GetById(expenseReportDetail.ExpenseId);
            expense.Total = expense.Details.Sum(p => p.Amount);

            _expenseReportRepository.Update(expense);

            return await _expenseReportRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Delete(Guid id)
        {
            _expenseReportDetailRepository.Delete(id);

            return await _expenseReportDetailRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _expenseReportRepository?.Dispose();
        }
    }
}
