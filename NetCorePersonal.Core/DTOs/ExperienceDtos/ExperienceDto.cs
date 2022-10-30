using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Core.DTOs.ExperienceDtos
{
    public class ExperienceDto : BaseDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsActived { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
    }
}
