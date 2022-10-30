using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Core.DTOs.UserDtos
{
    public class UserDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "{0} field must be  the most {1} character")]
        public string FullName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} field must be  the most {1} character")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} field must be  the most {1} character")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone number.")]
        public string Phone { get; set; }


        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        public string WebSite { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "{0} field with {1} field is not matching")]
        public string RePassword { get; set; }

        public string ProfileImage { get; set; } = "no-image.jpg";

        [Required]
        public string About { get; set; }

        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
