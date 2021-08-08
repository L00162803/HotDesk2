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
    public class IndexModel : PageModel
    {
        private readonly HotDeskContext _context;

        public IndexModel(HotDesk.Data.HotDeskContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string FromDateSort { get; set; }
        public string ToDateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }


        public IList<ResvDate> ResvDate { get;set; }
        public async Task OnGetAsync(string sortOrder)     
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            FromDateSort = sortOrder == "FDate" ? "fdate_desc" : "FDate";
            ToDateSort = sortOrder == "TDate" ? "tdate_desc" : "TDate";

            IQueryable<ResvDate> resvdatesIQ = from s in _context.ResvDate
                                             select s;

            switch (sortOrder)
            {
                case "name_desc":
                    resvdatesIQ = resvdatesIQ.OrderByDescending(s => s.Name);
                    break;
                case "FDate":
                    resvdatesIQ = resvdatesIQ.OrderBy(s => s.FromDate);
                    break;
                case "fdate_desc":
                    resvdatesIQ = resvdatesIQ.OrderByDescending(s => s.FromDate);
                    break;
                case "TDate":
                    resvdatesIQ = resvdatesIQ.OrderBy(s => s.ToDate);
                    break;
                case "tdate_desc":
                    resvdatesIQ = resvdatesIQ.OrderByDescending(s => s.ToDate);
                    break;
                default:
                    resvdatesIQ = resvdatesIQ.OrderBy(s => s.Name);
                    break;
            }

            ResvDate = await resvdatesIQ.AsNoTracking().ToListAsync();
        }
    }
}
