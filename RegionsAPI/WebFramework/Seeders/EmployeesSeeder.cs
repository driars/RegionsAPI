using Data;
using Data.Dtos;
using Data.Entities;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Services;

namespace WebFramework.Seeders
{
    public class EmployeesSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly String _rootPath;
        private readonly CacheService<EmployeeDto, Employee> _cacheService;
        private readonly Serilog.ILogger _logger;

        public EmployeesSeeder(ApplicationDbContext context, CacheService<EmployeeDto, Employee> cacheService, string rootPath, Serilog.ILogger logger)
        {
            _context = context;
            _rootPath = rootPath;
            _cacheService = cacheService;
            _logger = logger;
        }

        public void Seed()
        {
            if (_context.Regions.Any()) return;

            try
            {
                string filePath = Path.GetFullPath(Path.Combine(_rootPath, "../SeedData", "employees.csv"));
                int id = 1;

                using (TextFieldParser parser = new TextFieldParser(filePath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields()!;
                        EmployeeDto employee = new EmployeeDto();

                        employee.Name = fields[1];
                        employee.SurName = fields[2];
                        employee.RegionId = Int32.Parse(fields[0]);

                        _cacheService.Set(id++, employee);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }
    }
}
