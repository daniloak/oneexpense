using Microsoft.Extensions.DependencyInjection;
using OneExpense.Business.Interfaces;
using OneExpense.Business.Notifications;
using OneExpense.Business.Service;
using OneExpense.Data.Context;
using OneExpense.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneExpense.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies (this IServiceCollection services)
        {
            services.AddScoped<IExpenseRepository, ExpenseRepository>();

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<IExpenseService, ExpenseService>();

            return services;
        }
    }
}
