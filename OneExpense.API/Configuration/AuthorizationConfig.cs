using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using OneExpense.API.Authorization;

namespace OneExpense.API.Configuration
{
    public static class AuthorizationConfig
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, ExpenseAuthorizationHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ExpenseAuthorizationHandler.CAN_ACESS_EXPENSE, policyBuilder =>
                    policyBuilder.AddRequirements(new ExpenseUserRequirement()));
            });

            return services;
        }
    }
}
