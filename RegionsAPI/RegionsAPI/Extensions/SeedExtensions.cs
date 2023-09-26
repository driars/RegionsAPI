using Data.Dtos;
using Data.Entities;
using Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Seeders;
using WebFramework.Services;

namespace WebFramework.Extensions
{
    public static class SeedExtensions
    {
        public static IServiceProvider SeedData(this IServiceProvider provider, IServiceCollection services, Serilog.ILogger logger)
        {
            logger.Information("Start Seeding Data");

            using (var _provider = services.BuildServiceProvider())
            {
                try
                {
                    var context = _provider.GetRequiredService<ApplicationDbContext>();
                    var rootPath = provider.GetRequiredService<IWebHostEnvironment>().ContentRootPath;

                    new RegionsSeeder(context,
                        provider.GetRequiredService<CacheService<RegionDto, Region>>(),
                        rootPath, logger).Seed();

                    new EmployeesSeeder(context,
                        provider.GetRequiredService<CacheService<EmployeeDto, Employee>>(),
                        rootPath, logger).Seed();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.ToString());
                }
            }

            return provider;
        }
    }
}
