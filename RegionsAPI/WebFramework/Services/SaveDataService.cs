using AutoMapper;
using Data.Dtos;
using Data.Entities;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Services
{
    public class SaveDataService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly CacheService<RegionDto, Region> _regionCache;
        private readonly CacheService<EmployeeDto, Employee> _employeeCache;
        public SaveDataService(ApplicationDbContext context, IMapper mapper, CacheService<RegionDto, Region> regionCache, CacheService<EmployeeDto, Employee> employeeCache)
        {
            _context = context;
            _mapper = mapper;
            _regionCache = regionCache;
            _employeeCache = employeeCache;
        }

        public async Task SaveAsync()
        {
            await _regionCache.SaveToDatabase(_context, _context.Regions, _mapper);
            await _employeeCache.SaveToDatabase(_context, _context.Employees, _mapper);
        }
    }
}
