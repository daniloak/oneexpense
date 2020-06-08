using Microsoft.AspNetCore.Authorization;
using OneExpense.API.Extensions;
using OneExpense.API.Interfaces;
using OneExpense.Business.Models;
using System.Threading.Tasks;

namespace OneExpense.API.Authorization
{
    public class ExpenseUserRequirement : IAuthorizationRequirement
    {

    }

    public class ExpenseAuthorizationHandler : AuthorizationHandler<ExpenseUserRequirement, ExpenseReport>
    {
        public const string CAN_ACESS_EXPENSE = "CanAcessExpense";
        private readonly ICompanyUserService _companyUserService;

        public ExpenseAuthorizationHandler(ICompanyUserService companyUserService)
        {
            _companyUserService = companyUserService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExpenseUserRequirement requirement, ExpenseReport resource)
        {
            var userId = _companyUserService.UserId;

            if (resource.UserId == userId)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
