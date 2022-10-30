using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Core.DTOs.ProjectDtos
{
    public class ProjectDto : BaseDto
    {
        public string Name { get; set; }

        public string Client { get; set; }

        public string Image { get; set; } = "no-image.jpg";

    }
}
