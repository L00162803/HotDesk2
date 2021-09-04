// Author : Martin Connolly
//
// Description
// Details Available Desks controller

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotDesk.Data;
using HotDesk.Models;

namespace HotDesk.Pages.AvailDesks
{
    public class DetailsModel : PageModel
    {
        private readonly HotDesk.Data.HotDeskContext _context;

        public DetailsModel(HotDesk.Data.HotDeskContext context)
        {
            _context = context;
        }

        public AvailDesk AvailDesk { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AvailDesk = await _context.AvailDesk
                .Include(a => a.Desk)
                .Include(a => a.ResvDate).FirstOrDefaultAsync(m => m.ID == id);

            if (AvailDesk == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
