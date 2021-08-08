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
    public class IndexModel : PageModel
    {
        private readonly HotDesk.Data.HotDeskContext _context;

        public IndexModel(HotDesk.Data.HotDeskContext context)
        {
            _context = context;
        }

        public IList<AvailDesk> AvailDesk { get;set; }

        public async Task OnGetAsync()
        {
            AvailDesk = await _context.AvailDesk
                .Include(a => a.Desk)
                .Include(a => a.ResvDate).ToListAsync();
        }
    }
}
