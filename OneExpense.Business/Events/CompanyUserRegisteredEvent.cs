using OneExpense.Business.Messages.Integration;
using System;

namespace OneExpense.Business.Events
{
    public class CompanyUserRegisteredEvent : IntegrationEvent
    {
        public Guid CompanyId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string ConfirmationLink { get; set; }

        public CompanyUserRegisteredEvent(Guid companyId, string email, string password, string passwordConfirmation, string confirmationLink)
        {
            AggregateId = companyId;
            CompanyId = companyId;
            Email = email;
            Password = password;
            PasswordConfirmation = passwordConfirmation;
            ConfirmationLink = confirmationLink;
        }
    }
}
