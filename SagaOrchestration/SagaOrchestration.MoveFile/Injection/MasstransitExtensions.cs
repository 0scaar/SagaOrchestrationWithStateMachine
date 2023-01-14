using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SagaOrchestration.MoveFile.Injection
{
    public static class MasstransitExtensions
    {
        public static IServiceCollection ConfigureQueue(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                var entryAssembly = Assembly.GetEntryAssembly();

                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumers(entryAssembly);

                x.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
            });

            services.AddHostedService<Worker>();

            return services;
        }
    }
}
