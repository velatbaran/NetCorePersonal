using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Core.DTOs.CategoryDtos
{
    public class CategoryDto : BaseDto
    {
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
