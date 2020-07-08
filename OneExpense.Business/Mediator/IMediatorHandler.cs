using FluentValidation.Results;
using OneExpense.Business.Events;
using OneExpense.Business.Messages;
using System.Threading.Tasks;

namespace OneExpense.Business.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T appEvent) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
