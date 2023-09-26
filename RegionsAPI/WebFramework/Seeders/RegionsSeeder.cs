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
    public class RegionsSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly String _rootPath;
        private readonly CacheService<RegionDto, Region> _cacheService;

        public RegionsSeeder(ApplicationDbContext context, CacheService<RegionDto, Region> cacheService, string rootPath)
        {
            _context = context;
            _rootPath = rootPath;
            _cacheService = cacheService;
        }

        public void Seed()
        {
            if (_context.Regions.Any()) return;

            try
            {
                string filePath = Path.GetFullPath(Path.Combine(_rootPath, "../SeedData", "regions.csv"));

                using (TextFieldParser parser = new TextFieldParser(filePath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields()!;
                        RegionDto region = new RegionDto();

                        region.Id = Int32.Parse(fields[1]);
                        region.Name = fields[0];

                        if (!String.IsNullOrEmpty(fields[2]))
                        {
                            region.ParentId = Int32.Parse(fields[2]);
                        }

                        _cacheService.Set(region.Id, region);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
