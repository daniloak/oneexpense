using Microsoft.Extensions.DependencyInjection;
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
            services.AddScoped<IImageFileService, ImageFileService>();

            services.AddScoped<INotifier, Notifier>();

            services.AddScoped<IExpenseReportRepository, ExpenseReportRepository>();
            services.AddScoped<IExpenseReportDetailRepository, ExpenseReportDetailRepository>();

            services.AddScoped<IExpenseReportService, ExpenseReportService>();
            services.AddScoped<IExpenseReportDetailService, ExpenseReportDetailService>();

            return services;
        }
    }
}
