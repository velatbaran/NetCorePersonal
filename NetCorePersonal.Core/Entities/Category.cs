using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Core.Entities
{
    public class Category :BaseEntity
    {
        [Required]
        [StringLength(100,ErrorMessage ="{0} field must be  the most {1} character")]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public List<Project> Projects { get; set; }

        public Category()
        {
            Projects = new List<Project>();
        }
        
    }
}
