using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Core.DTOs.AccountDtos
{
    public class LoginDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "{0} field must be  the most {1} character")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
