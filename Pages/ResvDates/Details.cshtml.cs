using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotDesk.Data;
using HotDesk.Models;

namespace HotDesk.Pages.ResvDates
{
    public class DetailsModel : PageModel
    {
        private readonly HotDesk.Data.HotDeskContext _context;

        public DetailsModel(HotDesk.Data.HotDeskContext context)
        {
            _context = context;
        }

        public ResvDate ResvDate { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //ResvDate = await _context.ResvDate.FirstOrDefaultAsync(m => m.ID == id);
            ResvDate = await _context.ResvDate
                .Include(s => s.AvailDesks)
                .ThenInclude(e => e.Desk)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (ResvDate == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
