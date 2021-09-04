// Author : Martin Connolly
//
// Description
// Create Date Range controller

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotDesk.Data;
using HotDesk.Models;

namespace HotDesk.Pages.ResvDates
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
            return Page();
        }

        [BindProperty]
        public ResvDate ResvDate { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ResvDate.Add(ResvDate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
