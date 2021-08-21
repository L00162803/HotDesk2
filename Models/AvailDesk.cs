using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Models
{
    public class AvailDesk
    {
        public int ID { get; set; }
        public int  ResvDateID { get; set; }
        public int DeskID { get; set; }
        [Display(Name = "Available Desks")]

        public ResvDate ResvDate { get; set; }
        public Desk Desk { get; set; }
    }
}
