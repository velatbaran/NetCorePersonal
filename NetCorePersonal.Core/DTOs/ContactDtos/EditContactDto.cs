using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Core.DTOs.ContactDtos
{
    public class EditContactDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        public string Location { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} field must be  the most {1} character")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone number.")]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        public string Instagram { get; set; }

        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        public string Twitter { get; set; }

        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        public string Linkedin { get; set; }

        [StringLength(100, ErrorMessage = "{0} field must be  the most {1} character")]
        public string Telegram { get; set; }

        [Required]
        public string IFrame { get; set; }

        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
