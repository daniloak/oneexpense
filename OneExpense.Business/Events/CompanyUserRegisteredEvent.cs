using System;
using System.Collections.Generic;
using System.Text;

namespace OneExpense.Business.Events
{
    public class CompanyUserRegisteredEvent : Event
    {
        public Guid CompanyId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

        public CompanyUserRegisteredEvent(Guid companyId, string email, string password, string passwordConfirmation)
        {
            AggregateId = companyId;
            CompanyId = companyId;
            Email = email;
            Password = password;
            PasswordConfirmation = passwordConfirmation;
        }
    }
}
