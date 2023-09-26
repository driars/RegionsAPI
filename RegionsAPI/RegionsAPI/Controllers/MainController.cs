using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegionsAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class MainController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<MainController> _logger;
        private readonly IMapper _mapper;
        private readonly CacheService<RegionDto, Region> _regionCache;
        private readonly CacheService<EmployeeDto, Employee> _employeeCache;

        public MainController(ApplicationDbContext context, IMapper mapper, ILogger<MainController> logger, CacheService<RegionDto, Region> regionCache, CacheService<EmployeeDto, Employee> employeeCache)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _employeeCache = employeeCache;
            _regionCache = regionCache;
        }

        [HttpGet("region/{id}/employees")]
        public async Task<ActionResult<IEnumerable<EmployeeSelectDto>>> GetEmployeesByRegion(int id)
        {
            IEnumerable<RegionDto> regions = _regionCache.GetAll();

            if (_context.Regions.Any())
            {
                regions = _context.Regions.AsNoTracking().ProjectTo<RegionDto>(_mapper.ConfigurationProvider);
            }

            var item = regions.FirstOrDefault(e => e.Id == id);
            if (item == null) return NotFound();

            // Use BFS to get all descendant ids
            Stack<Int32> stack = new Stack<Int32>();
            stack.Push(item.Id);

            HashSet<Int32> ids = new HashSet<Int32>();

            while (stack.Count > 0)
            {
                int currentId = stack.Pop();
                ids.Add(currentId);

                foreach (var r in regions.Where(e => e.ParentId == currentId))
                {
                    if (ids.Contains(r.Id)) continue;

                    stack.Push(r.Id);
                }
            }

            if (_context.Employees.Any())
            {
                return await _context.Employees.AsNoTracking().Where(e => ids.Contains(e.RegionId)).
                    ProjectTo<EmployeeSelectDto>(_mapper.ConfigurationProvider).ToListAsync();
            }

            return Ok(_employeeCache.GetAll().Where(e => ids.Contains(e.RegionId)).Select(e => new EmployeeSelectDto
            {
                Name = e.Name,
                SurName = e.SurName,
                RegionName = _regionCache.Get(e.RegionId).Name
            }));
        }

        // GET: api/<MainController>
        [HttpGet("regions")]
        public async Task<ActionResult<IEnumerable<RegionSelectDto>>> GetRegions()
        {
            if (_context.Regions.Any()) {
                return await _context.Regions.AsNoTracking().ProjectTo<RegionSelectDto>(_mapper.ConfigurationProvider).ToListAsync();
            }

            var regions = _regionCache.GetAll();

            return Ok(regions.Select(e => new RegionSelectDto
            {
                Id = e.Id,
                Name = e.Name,
                ParentName = e.ParentId != null ? _regionCache.Get(e.ParentId ?? 0).Name : null,
            }));
        }

        [HttpGet("employees")]
        public async Task<ActionResult<IEnumerable<EmployeeSelectDto>>> GetEmployees()
        {
            if (_context.Employees.Any())
            {
                return await _context.Employees.AsNoTracking().ProjectTo<EmployeeSelectDto>(_mapper.ConfigurationProvider).ToListAsync();
            }

            var employees = _employeeCache.GetAll();

            return Ok(employees.Select(e => new EmployeeSelectDto
            {
                Name = e.Name,
                SurName = e.SurName,
                RegionName = _regionCache.Get(e.RegionId).Name
            }));
        }

        [HttpPost("region")]
        public async Task<IActionResult> Post([FromBody] RegionDto regionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Region region = _mapper.Map<Region>(regionDto);

            if (!_context.Regions.Any())
                await new SaveDataService(_context, _mapper, _regionCache, _employeeCache).SaveAsync();

            region.Id = _context.Regions.Max(e => e.Id) + 1;

            _context.Regions.Add(region);
            await _context.SaveChangesAsync();

            return CreatedAtAction(null, region);
        }

        [HttpPost("employee")]
        public async Task<IActionResult> Post([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Employee employee = _mapper.Map<Employee>(employeeDto);

            if (!_context.Employees.Any())
                await new SaveDataService(_context, _mapper, _regionCache, _employeeCache).SaveAsync();

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(null, employee);
        }
    }
}
