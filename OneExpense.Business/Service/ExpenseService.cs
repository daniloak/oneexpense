using OneExpense.Business.Interfaces;
using OneExpense.Business.Models;
using OneExpense.Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace OneExpense.Business.Service
{
    public class ExpenseService : BaseService, IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository,
                                INotifier notifier) : base(notifier)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task Add(Expense expense)
        {
            if (!Validate(new ExpenseValidation(), expense)) return;

            await _expenseRepository.Add(expense);
        }

        public async Task Update(Expense expense)
        {
            if (!Validate(new ExpenseValidation(), expense)) return;

            await _expenseRepository.Update(expense);
        }

        public async Task Delete(Guid id)
        {
            await _expenseRepository.Delete(id);
        }

        public void Dispose()
        {
            _expenseRepository?.Dispose();
        }
    }
}
