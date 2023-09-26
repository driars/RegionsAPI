using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Region : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public int? ParentId { get; set; }
        public Region? Parent { get; set; }
        public IEnumerable<Region>? Children { get; set; }

        public required IEnumerable<Employee> Employees { get; set; }
    }
}
