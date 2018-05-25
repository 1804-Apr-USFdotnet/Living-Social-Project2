using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateCRM.Models
{
    public class User : IEntity
    {
        [Column("Id")]
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Alias { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

    }
}
