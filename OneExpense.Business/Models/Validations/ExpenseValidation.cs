using FluentValidation;

namespace OneExpense.Business.Models.Validations
{
    public class ExpenseValidation : AbstractValidator<Expense>
    {
        public ExpenseValidation()
        {
            RuleFor(f => f.Amount)
                .GreaterThan(0)
                .WithMessage("Valor deve ser maior que zero");
        }
    }
}
