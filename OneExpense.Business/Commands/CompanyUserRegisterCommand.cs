using OneExpense.Business.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneExpense.Business.Commands
{
    public class CompanyUserRegisterCommand : Command
    {
        public Guid CompanyId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

        public CompanyUserRegisterCommand(Guid companyId, string email, string password, string passwordConfirmation)
        {
            AggregateId = companyId;
            Email = email;
            Password = password;
            PasswordConfirmation = passwordConfirmation;
        }
    }
}
