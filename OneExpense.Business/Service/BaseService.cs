using FluentValidation;
using FluentValidation.Results;
using OneExpense.Business.Interfaces;
using OneExpense.Business.Models;
using OneExpense.Business.Notifications;

namespace OneExpense.Business.Service
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        public BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected bool Validate<TV, TE>(TV validation, TE entity) 
            where TV : AbstractValidator<TE> 
            where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}
