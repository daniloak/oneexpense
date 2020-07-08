using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneExpense.Business.Messages
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string error)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
        }
    }
}
