using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class EmployeeDto : BaseDto
    {
        public string Name { get; set; } = "";
        public string SurName { get; set; } = "";
        public int RegionId { get; set; }
    }
}
