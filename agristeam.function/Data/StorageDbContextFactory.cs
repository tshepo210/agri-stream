using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace agristeam.function.Data;

public sealed class StorageDbContextFactory : IDesignTimeDbContextFactory<StorageDbContext>
{
    public StorageDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable(
            "ConnectionStrings__AzureSqlConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = ReadConnectionStringFromLocalSettings();
        }

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Azure SQL connection string is missing. Set ConnectionStrings__AzureSqlConnection " +
                "or add it to the Values section of local.settings.json.");
        }

        var options = new DbContextOptionsBuilder<StorageDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        return new StorageDbContext(options);
    }

    private static string? ReadConnectionStringFromLocalSettings()
    {
        var paths = new[]
        {
            Path.Combine(Directory.GetCurrentDirectory(), "local.settings.json"),
            Path.Combine(AppContext.BaseDirectory, "local.settings.json")
        };

        foreach (var path in paths.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            if (!File.Exists(path))
            {
                continue;
            }

            using var document = JsonDocument.Parse(File.ReadAllText(path));
            if (document.RootElement.TryGetProperty("Values", out var values) &&
                values.TryGetProperty("ConnectionStrings__AzureSqlConnection", out var value))
            {
                return value.GetString();
            }
        }

        return null;
    }
}
