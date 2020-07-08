using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OneExpense.API.Extensions;
using OneExpense.API.Interfaces;
using OneExpense.API.Services;
using OneExpense.Business.Events;
using OneExpense.Business.Interfaces;
using OneExpense.Business.Mediator;
using OneExpense.Business.Notifications;
using OneExpense.Business.Service;
using OneExpense.Data.Repository;

namespace OneExpense.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            //services.AddScoped<IRequestHandler<CompanyUserRegisterCommand, ValidationResult>, CompanyUserRegisterCommandHandler>();
            services.AddScoped<INotificationHandler<CompanyUserRegisteredEvent>, CompanyUserEventHandler>();

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
