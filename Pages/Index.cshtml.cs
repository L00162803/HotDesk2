using HotDesk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDesk.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HotDesk.Data.HotDeskContext _context;
        [BindProperty]
        public List<SelectListItem> UserList { get; set; }
        [BindProperty]
        public int selectedUserID { get; set; } = 0;
        static int activeUser;
        public IndexModel(ILogger<IndexModel> logger, HotDesk.Data.HotDeskContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            // LINQ to return list of users
            UserList = _context.User.Select(u =>                                    
                                    new SelectListItem
                                    {
                                        Value = u.ID.ToString(),
                                        Text = u.LogonName,                                        
                                    }).ToList();
            

            selectedUserID = activeUser;
        }
        public async Task<IActionResult> OnPost()            
        {
            activeUser = selectedUserID;
            // User Type
            List<User> selectedUser = (from r in _context.User
                                               where r.ID == selectedUserID
                                               select r).ToList();
            String userType = selectedUser[0].Type;
            String userName = selectedUser[0].FirstName + " "+ selectedUser[0].LastName;
            HttpContext.Session.SetString("ActiveUser",activeUser.ToString());
            HttpContext.Session.SetString("ActiveUserType", userType);
            HttpContext.Session.SetString("ActiveUserName",userName);
            return RedirectToAction("OnGet");
            
        }

    }
}
