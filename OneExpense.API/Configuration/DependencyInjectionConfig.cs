using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using OneExpense.API.Authorization;
using OneExpense.API.Extensions;
using OneExpense.API.Interfaces;
using OneExpense.API.Services;
using OneExpense.Business.Interfaces;
using OneExpense.Business.Notifications;
using OneExpense.Business.Service;
using OneExpense.Data.Repository;

namespace OneExpense.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICompanyUserService, CompanyUserService>();
            services.AddScoped<INotifier, Notifier>();

            services.AddScoped<IImageFileService, ImageFileService>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IExpenseReportRepository, ExpenseReportRepository>();
            services.AddScoped<IExpenseReportDetailRepository, ExpenseReportDetailRepository>();

            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IExpenseReportService, ExpenseReportService>();
            services.AddScoped<IExpenseReportDetailService, ExpenseReportDetailService>();

            return services;
        }
    }
}
