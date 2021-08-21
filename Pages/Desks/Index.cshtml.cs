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

namespace HotDesk.Pages.Desks
{
    public class IndexModel : PageModel
    {
        private readonly HotDesk.Data.HotDeskContext _context;

        public IndexModel(HotDesk.Data.HotDeskContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string LocationSort { get; set; }
        public string CategorySort { get; set; }
        public string CurrentSort { get; set; }

        public IList<Desk> Desk { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string DeskCategory { get; set; }
        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            LocationSort = String.IsNullOrEmpty(sortOrder) ? "location_desc" : "";
            CategorySort = String.IsNullOrEmpty(sortOrder) ? "category_desc" : "";

            IQueryable<Desk> desksIQ = from s in _context.Desk
                                               select s;

            switch (sortOrder)
            {
                case "name_desc":
                    desksIQ = desksIQ.OrderByDescending(s => s.Name);
                    break;
                case "location_desc":
                    desksIQ = desksIQ.OrderByDescending(s => s.Location);
                    break;
                case "category_desc":
                    desksIQ = desksIQ.OrderByDescending(s => s.Category);
                    break;
                default:
                    desksIQ = desksIQ.OrderBy(s => s.Name);
                    break;
            }

            Desk = await desksIQ.AsNoTracking().ToListAsync();
        }
    }
}
