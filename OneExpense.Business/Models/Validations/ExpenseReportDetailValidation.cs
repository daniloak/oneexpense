using FluentValidation;
using System;

namespace OneExpense.Business.Models.Validations
{
    public class ExpenseReportDetailValidation : AbstractValidator<ExpenseReportDetail>
    {
        public ExpenseReportDetailValidation()
        {
            RuleFor(f => f.Amount)
                .GreaterThan(0)
                .WithMessage("Valor despesa deve ser maior que zero");
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default);
        }
    }
}
