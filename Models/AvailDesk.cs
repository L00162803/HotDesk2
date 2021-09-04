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
    // Model storing associated desks and date ranges
    public class AvailDesk
    {
        public int ID { get; set; }
        // Date Range ID
        public int  ResvDateID { get; set; }
        // Desk ID
        public int DeskID { get; set; }
        //Date Range Object
        [Display(Name = "Available Desks")]

        public ResvDate ResvDate { get; set; }
        // Desk Object
        public Desk Desk { get; set; }
    }
}
