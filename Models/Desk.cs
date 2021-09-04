using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Models
{
    // Author : Martin Connolly
    //
    // Description
    // Model for Desk objects
    public class Desk
    {
        public int ID { get; set; }
        //Desk Name
        [Display(Name = "Desk Name")]
        public string Name { get; set; }
        //Desk Location
        public string Location { get; set; }
        //Desk Category
        public string Category { get; set; }
        //Associated Date Range objects
        public ICollection<AvailDesk> AvailDesks { get; set; }

    }
}
