using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PSUFunctionsDemoProject.FunctionsBlob;
using PSUFunctionsDemoProject.FunctionsTableStorage;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        //custom repositories
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IFileRepository, FileRepository>();
    })
    .Build();

host.Run();
