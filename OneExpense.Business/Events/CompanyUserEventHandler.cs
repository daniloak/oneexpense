using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OneExpense.Business.Events
{
    public class CompanyUserEventHandler : INotificationHandler<CompanyUserRegisteredEvent>
    {
        public Task Handle(CompanyUserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
