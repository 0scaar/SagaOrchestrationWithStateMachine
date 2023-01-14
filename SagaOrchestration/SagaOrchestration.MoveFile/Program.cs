using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SagaOrchestration.MoveFile.Injection;

var services = new ServiceCollection();
services.ConfigureQueue();

using IHost host = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder => builder.AddAutofacRegistration(services))
    .Build();

await host.RunAsync();