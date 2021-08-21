using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Models
{
    public class Desk
    {
        public int ID { get; set; }
        [Display(Name = "Desk Name")]
        public string Name { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }

        public ICollection<AvailDesk> AvailDesks { get; set; }

    }
}
