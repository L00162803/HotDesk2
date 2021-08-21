using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]

        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(1)]
        [RegularExpression("[AE]")]
        public string Type { get; set; }
        [Display(Name = "Logon Name")]
        [MinLength(5)]
        public string LogonName { get; set; }
    }
}
