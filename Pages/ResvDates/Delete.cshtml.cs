// Author : Martin Connolly
//
// Description
// Delete Date Range controller

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
    public class DeleteModel : PageModel
    {
        private readonly HotDesk.Data.HotDeskContext _context;

        public DeleteModel(HotDesk.Data.HotDeskContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ResvDate ResvDate { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ResvDate = await _context.ResvDate.FirstOrDefaultAsync(m => m.ID == id);

            if (ResvDate == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ResvDate = await _context.ResvDate.FindAsync(id);

            if (ResvDate != null)
            {
                _context.ResvDate.Remove(ResvDate);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
