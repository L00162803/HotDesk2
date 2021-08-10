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

        public IList<Desk> Desk { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string DeskCategory { get; set; }
        public async Task OnGetAsync()
        {
            // LINQ to return list of desk categories
            IQueryable<string> categoryQuery = from m in _context.Desk
                                            orderby m.Category
                                            select m.Category;
            var desks = from d in _context.Desk
                         select d;
            if (!string.IsNullOrEmpty(SearchString))
            {
                desks = desks.Where(s => s.Category.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(DeskCategory))
            {
                desks = desks.Where(x => x.Category == DeskCategory);
            }
            Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
            Desk = await desks.ToListAsync();
        }
    }
}
