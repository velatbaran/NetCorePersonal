using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Core.Entities
{
    public class Project : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        public string Client { get; set; }

        [Required]
        [StringLength(250, ErrorMessage = "{0} field must be  the most {1} character")]
        public string UsedTeknologies { get; set; }

        public string ProjectUrl { get; set; }

        public string Image { get; set; } = "no-image.jpg";

        [Required]
        public string Description { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
