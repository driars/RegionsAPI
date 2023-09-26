using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string SurName { get; set; } = "";

        public int RegionId { get; set; }
        public required Region Region { get; set; }
    }
}
