using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneExpense.API.Consumers;
using OneExpense.Business.Events;
using System;

namespace OneExpense.API.Configuration
{
    public static class MessageBusConfig
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            var userRegisteredQueue = configuration.GetValue<string>("UserRegisteredQueue");
            EndpointConvention.Map<CompanyUserRegisteredEvent>(
                   new Uri(userRegisteredQueue));

            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserRegisteredConsumer>();

                x.UsingAzureServiceBus((context, cfg) =>
                {
                    var queueHost = configuration.GetValue<string>("QueueHost");
                    cfg.Host(queueHost);
                    cfg.ReceiveEndpoint("user-registered", e =>
                    {
                        e.SelectBasicTier();
                        e.ConfigureConsumeTopology = false;
                        e.ConfigureConsumer<UserRegisteredConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}
