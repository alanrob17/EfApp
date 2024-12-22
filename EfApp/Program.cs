using System;
using System.IO;
using EfApp.Models;
using EfApp.Repositories;
using EfApp.Services;
using EfApp.Tests;
using EfApp.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EfApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Set up Dependency Injection
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddDbContext<AppDbContext>() // Register DbContext
                .AddScoped<IArtistRepository, ArtistRepository>() // Register repository
                .AddScoped<IRecordRepository, RecordRepository>()
                .AddScoped<IStatisticRepository, StatisticRepository>()
                .AddScoped<ArtistService>() // Register ArtistService
                .AddScoped<RecordService>() // Register ArtistService
                .AddScoped<StatisticService>() // Register StatisticService
                .AddScoped<AppLogger>() // Register AppLogger
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConsole(); // Add console logging
                })
                .BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var artistService = scope.ServiceProvider.GetRequiredService<ArtistService>();
                var recordService = scope.ServiceProvider.GetRequiredService<RecordService>();
                var statisticService = scope.ServiceProvider.GetRequiredService<StatisticService>();
                var appLogger = scope.ServiceProvider.GetRequiredService<AppLogger>();

                var artistTest = new ArtistTest(artistService, appLogger);
                await artistTest.RunTestsAsync();

                var recordTest = new RecordTest(recordService, appLogger);
                await recordTest.RunTestsAsync();

                var artistRecordTest = new ArtistRecordTest(artistService, recordService, appLogger);
                await artistRecordTest.RunTestsAsync();

                var statisticTest = new StatisticTest(statisticService, appLogger);
                await statisticTest.RunTestsAsync();
            }
        }
    }
}
