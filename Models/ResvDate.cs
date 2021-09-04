using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Models
{
    public class ResvDate
    {
        // Author : Martin Connolly
        //
        // Description
        // Model for date ranges

        public int ID { get; set; }
        //Range Name
        [Display(Name = "Range Name")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        //From date
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }
        //To date
        [DataType(DataType.Date)]
        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }
        //Associated desk objects
        [Display(Name = "Available Desks")]
        public ICollection<AvailDesk> AvailDesks { get; set; }
    }
}
