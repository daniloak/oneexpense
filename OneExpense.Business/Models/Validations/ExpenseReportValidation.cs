using FluentValidation;

namespace OneExpense.Business.Models.Validations
{
    public class ExpenseReportValidation : AbstractValidator<ExpenseReport>
    {
        public ExpenseReportValidation()
        {
            RuleFor(f => f.Description)
               .NotEmpty()
               .WithMessage("Usuário deve preencher o campo descrição");
            //RuleFor(f => f.Total)
            //    .GreaterThan(0)
            //    .WithMessage("Total deve ser maior que zero");
        }
    }
}
