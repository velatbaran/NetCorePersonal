using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Core.DTOs.ExperienceDtos
{
    public class EditExperienceDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        public string Name { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "{0} field must be  the most {1} character")]
        public string Location { get; set; }

        public bool IsActived { get; set; } = false;

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
