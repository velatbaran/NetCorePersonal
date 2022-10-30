using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Core.DTOs.AccountDtos
{
    public class ForgotPasswordDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
    }
}
