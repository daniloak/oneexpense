using MassTransit;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Hosting;
using OneExpense.Business.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OneExpense.API.Consumers
{
    public class UserRegisteredConsumer : IConsumer<CompanyUserRegisteredEvent>
    {
        private readonly IEmailSender _emailSender;
        public UserRegisteredConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Consume(ConsumeContext<CompanyUserRegisteredEvent> context)
        {
            await _emailSender.SendEmailAsync(context.Message.Email, "Email confirmation", context.Message.ConfirmationLink);
        }
    }
}
