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
    // Model for a reservation entr
    public class Reservation
    {
        public int ID { get; set; }
        // User for reservation
        public int UserID { get; set; }
        // Desk for reservation
        public int DeskID { get; set; }
        //Date for reservation
        [DataType(DataType.Date)]        
        public DateTime ResvDate { get; set; }

    }
}
