using AutoMapper;
using Data.Dtos;
using Data.Entities;
using Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace WebFramework.Services
{
    public class SaveBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _provider;
        private readonly IMapper _mapper;
        public SaveBackgroundService(IServiceProvider provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Run after 5 minutes

                using (var scope = _provider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    if (_context.Regions.Any()) return;

                    var regionService = scope.ServiceProvider.GetRequiredService<CacheService<RegionDto, Region>>();
                    var employeeService = scope.ServiceProvider.GetRequiredService<CacheService<EmployeeDto, Employee>>();

                    await new SaveDataService(_context, _mapper, regionService, employeeService).SaveAsync();
                }

                break;
            }
        }
    }
}
