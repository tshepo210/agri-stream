using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace agristeam.web.Data
{
    public class AppDbContextFactory
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable(
                "ConnectionStrings__DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = ReadConnectionStringFromLocalSettings();
            }

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Azure SQL connection string is missing. Set ConnectionStrings__DefaultConnection " +
                    "or add it to the Values section of local.settings.json.");
            }

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            return new AppDbContext(options);
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
}
