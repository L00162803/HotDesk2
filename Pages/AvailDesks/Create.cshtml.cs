// Author : Martin Connolly
//
// Description
// Create Available Desks controller

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotDesk.Data;
using HotDesk.Models;

namespace HotDesk.Pages.AvailDesks
{
    public class CreateModel : PageModel
    {
        private readonly HotDesk.Data.HotDeskContext _context;

        public CreateModel(HotDesk.Data.HotDeskContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["DeskID"] = new SelectList(_context.Desk, "ID", "ID");
        ViewData["DeskName"] = new SelectList(_context.Desk, "ID", "Name");

        ViewData["ResvDateID"] = new SelectList(_context.ResvDate, "ID", "ID");
        ViewData["ResvDateName"] = new SelectList(_context.ResvDate, "ID", "Name");
        return Page();
        }

        [BindProperty]
        public AvailDesk AvailDesk { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AvailDesk.Add(AvailDesk);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
