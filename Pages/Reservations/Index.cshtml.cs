// Author : Martin Connolly
//
// Description
// Main reservations grid

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotDesk.Data;
using HotDesk.Models;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace HotDesk.Pages.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly HotDesk.Data.HotDeskContext _context;
        public ReservationsViewModel ViewModel { get; set; }
        [BindProperty]
        public DateTime selectedDate { get; set; } = new DateTime(2021, 1, 10);        
        //static DateTime startDate = new DateTime(2021, 1, 10);
        static DateTime startDate = DateTime.Today;

        public IndexModel(HotDesk.Data.HotDeskContext context)
        {
            _context = context;
        }

        public IList<Reservation> Reservation { get; set; }

        public async Task OnGetAsync()
        {
            var viewModel = new ReservationsViewModel();

            //Get list of desks
            List<Desk> deskList = (from d in _context.Desk
                                   orderby d.Name
                                   select d).ToList();

            DateTime checkDate = startDate;
            var deskStatus = new DeskStatus();           
            //int userID = 1;
            int userID = Convert.ToInt32(HttpContext.Session.GetString("ActiveUser"));
            viewModel.SelectedDate = startDate;
            //For each desk check status for next seven days
            foreach (Desk checkDesk in deskList)
            {
                DeskReservation deskReservation = new DeskReservation();
                deskReservation.DeskID = checkDesk.ID;
                deskReservation.DeskName = checkDesk.Name;
                for (int i = 0; i < 7; i++)
                {
                    checkDate = startDate.AddDays(i);
                    // List of date ranges containing check date
                    List<ResvDate> containingRanges = (from r in _context.ResvDate
                                                       where r.FromDate <= checkDate && r.ToDate >= checkDate
                                                       select r).ToList();
                    if (containingRanges.Count == 0)
                    {
                        deskStatus = DeskStatus.UnAvailable;
                    }
                    else
                    {
                        //Is there a reservation for that desk / date combo?

                        List<Reservation> deskReservations = (from r in _context.Reservation
                                                              where r.DeskID == checkDesk.ID && r.ResvDate == checkDate
                                                              select r).ToList();
                        if (deskReservations.Count > 0 && deskReservations.ElementAt(0).UserID.Equals(userID))
                        {
                            deskStatus = DeskStatus.BookedByMe;
                        }
                        if (deskReservations.Count > 0 && !deskReservations.ElementAt(0).UserID.Equals(userID))
                        {
                            deskStatus = DeskStatus.BookedByOther;
                        }
                        if (deskReservations.Count == 0)
                        {
                            deskStatus = DeskStatus.Available;
                        }
                    }
                    DeskDateStatus deskDateStatus = new DeskDateStatus();
                    deskDateStatus.ReservationDate = checkDate;
                    deskDateStatus.ReservationStatus = deskStatus;
                    deskReservation.AddStatus(deskDateStatus);
                    //deskReservation.DeskDateStatuses.Add(deskDateStatus);


                }
                viewModel.AddDeskReservation(deskReservation);
            }
            this.ViewModel = viewModel;
        }
        public async Task<IActionResult> OnPost()
        {
            startDate = selectedDate;
            return RedirectToAction("OnGet");
        }
        public async Task<IActionResult> OnPostDateChange()
        {
            startDate = selectedDate;
            return RedirectToAction("OnGet");
        }

        public async Task<IActionResult> ChangeStatus(int deskID, DateTime resDate)
        {
            
            return RedirectToAction("OnGet");
        }

        public async Task<IActionResult> OnGetChangeReservationStatus(String resDate,int id, DeskStatus status)
        {
            int deskId = id;
            int userID = Convert.ToInt32(HttpContext.Session.GetString("ActiveUser"));

            DateTime resvDate = DateTime.ParseExact(resDate, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (status==DeskStatus.Available)
            {
                var newReservation = new Reservation();
                newReservation.DeskID = id;
                newReservation.ResvDate = resvDate;
                newReservation.UserID = userID;
                _context.Reservation.Add(newReservation);
                await _context.SaveChangesAsync();
            }
            if (status == DeskStatus.BookedByMe)
            {
                List<Reservation> delReservation = (from r in _context.Reservation
                                    where r.ResvDate == resvDate && r.DeskID == id && r.UserID==userID
                                    select r).ToList();
                _context.Reservation.Remove(delReservation[0]);
                await _context.SaveChangesAsync();
            }


            return RedirectToAction("OnGet");
        }

    }
    public enum DeskStatus
    {
        [Display(Name="Available")]
        Available = 0,
        [Display(Name = "Unavailable")]
        UnAvailable = 1,
        [Display(Name = "Mine")]
        BookedByMe = 2,
        [Display(Name = "Other")]
        BookedByOther = 3
    }
    public class DeskReservation
    {
        public int DeskID { get; set; }
        public string DeskName { get; set; }
        private List<DeskDateStatus> deskDateStatuses = new List<DeskDateStatus>();
        public List<DeskDateStatus> DeskDateStatuses {
            //get => this.deskDateStatuses == null ? new List<DeskDateStatus>() : this.deskDateStatuses; // Ternary operator
            get => this.deskDateStatuses;
             
        }
        public void AddStatus(DeskDateStatus deskDatestatus)
        {
            this.DeskDateStatuses.Add(deskDatestatus);
        }
    }

    public class DeskDateStatus
    {
        public DateTime ReservationDate { get; set; }
        public DeskStatus ReservationStatus { get; set; }
    }

    public class ReservationsViewModel 
    {
        public DateTime SelectedDate { get; set; }
        private List<DeskReservation> deskReservations = new List<DeskReservation>();
        public List<DeskReservation> DeskReservations { get => this.deskReservations; }
        public void AddDeskReservation(DeskReservation deskReservation)
        {
            this.DeskReservations.Add(deskReservation);
        }
    }
}
