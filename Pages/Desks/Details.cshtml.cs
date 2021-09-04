// Author : Martin Connolly
//
// Description
// Details desks controller

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotDesk.Data;
using HotDesk.Models;

namespace HotDesk.Pages.Desks
{
    public class DetailsModel : PageModel
    {
        private readonly HotDesk.Data.HotDeskContext _context;

        public DetailsModel(HotDesk.Data.HotDeskContext context)
        {
            _context = context;
        }

        public Desk Desk { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Desk = await _context.Desk.FirstOrDefaultAsync(m => m.ID == id);

            Desk = await _context.Desk
                .Include(s => s.AvailDesks)
                .ThenInclude(e => e.ResvDate)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);


            if (Desk == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
