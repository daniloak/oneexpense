using Microsoft.AspNetCore.Identity;
using OneExpense.Business.Models;
using OneExpense.Data;
using OneExpense.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneExpense.API.Validators
{
    public class CompanyUserValidator : IUserValidator<CompanyUser>
    {
        private readonly OneExpenseDbContext _dbContext;
        public CompanyUserValidator(OneExpenseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<IdentityResult> ValidateAsync(UserManager<CompanyUser> manager, CompanyUser user)
        {
            if (_dbContext.Companies.Any(p => p.Id == user.CompanyId))
            {
                return Task.FromResult(IdentityResult.Success);
            }

            return Task.FromResult(
                 IdentityResult.Failed(new IdentityError
                 {
                     Code = "InvalidCompany",
                     Description = "Company is invalid."
                 }));
        }
    }
}
