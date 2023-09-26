using Data;
using Data.Dtos;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<MainController> _logger;
        private readonly CacheService<RegionDto, Region> _regionCache;
        private readonly CacheService<EmployeeDto, Employee> _employeeCache;

        public MainController(ApplicationDbContext context, ILogger<MainController> logger, CacheService<RegionDto, Region> regionCache, CacheService<EmployeeDto, Employee> employeeCache)
        {
            _context = context;
            _logger = logger;
            _employeeCache = employeeCache;
            _regionCache = regionCache;
        }

        // GET: api/<MainController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MainController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MainController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MainController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MainController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
