using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotDesk.Data;
using HotDesk.Models;

namespace HotDesk.Pages.AvailDesks
{
    public class EditModel : PageModel
    {
        private readonly HotDesk.Data.HotDeskContext _context;

        public EditModel(HotDesk.Data.HotDeskContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["DeskID"] = new SelectList(_context.Desk, "ID", "ID");
           ViewData["ResvDateID"] = new SelectList(_context.ResvDate, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AvailDesk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AvailDeskExists(AvailDesk.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AvailDeskExists(int id)
        {
            return _context.AvailDesk.Any(e => e.ID == id);
        }
    }
}
