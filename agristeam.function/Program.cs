using agristeam.function.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        // Application Insights
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        // Azure SQL Database
        var connectionString = Environment.GetEnvironmentVariable(
            "ConnectionStrings__AzureSqlConnection"
        );

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Azure SQL connection string is missing."
            );
        }

        services.AddDbContext<StorageDbContext>(options =>
            options.UseSqlServer(connectionString)
        );
    })
    .Build();

host.Run();
