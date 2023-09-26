using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class RegionDto : BaseDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = "";
        public int? ParentId { get; set; }
    }

    public class RegionSelectDto : BaseDto
    {
        public string Name { get; set; } = "";
        public string? ParentName { get; set; } = "";
    }
}
