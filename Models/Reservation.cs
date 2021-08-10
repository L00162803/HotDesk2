using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int DeskID { get; set; }
        [DataType(DataType.Date)]        
        public DateTime ResvDate { get; set; }

    }
}
