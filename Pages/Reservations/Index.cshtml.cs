using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotDesk.Data;
using HotDesk.Models;

namespace HotDesk.Pages.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly HotDesk.Data.HotDeskContext _context;

        public IndexModel(HotDesk.Data.HotDeskContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservation { get;set; }
        public IList<Desk> Desk { get; set; }

        public async Task OnGetAsync()
        {
            Reservation = await _context.Reservation.ToListAsync();
            Desk = await _context.Desk.ToListAsync();
        }
    }
}
