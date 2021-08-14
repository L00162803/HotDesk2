using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotDesk.Data;
using HotDesk.Models;

namespace HotDesk.Pages.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly HotDesk.Data.HotDeskContext _context;

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
            var startDate = new DateTime(2021, 1, 10);
            var checkDate = startDate;
            var deskStatus = new DeskStatus();
            int userID = 1;
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
                    if(containingRanges.Count==0)
                    {
                        deskStatus = DeskStatus.UnAvailable;
                    }
                    else
                    {
                        //Is there a reservation for that desk / date combo?

                        List<Reservation> deskReservations = (from r in _context.Reservation
                                                              where r.DeskID == checkDesk.ID && r.ResvDate >= checkDate
                                                              select r).ToList();
                        if (deskReservations.Count > 0 && deskReservations.ElementAt(0).UserID.Equals(userID))
                        {
                            deskStatus = DeskStatus.BookedByMe;
                        }
                        if (deskReservations.Count > 0 && !deskReservations.ElementAt(0).UserID.Equals(userID))
                        {
                            deskStatus = DeskStatus.BookedByOther;
                        }
                        if (deskReservations.Count == 0 )
                        {
                            deskStatus = DeskStatus.Available;
                        }
                        DeskDateStatus deskDateStatus = new DeskDateStatus();
                        deskDateStatus.ReservationDate = checkDate;
                        deskDateStatus.ReservationStatus = deskStatus;
                        deskReservation.AddStatus(deskDateStatus);
                        //deskReservation.DeskDateStatuses.Add(deskDateStatus);
                    }

                }
                viewModel.AddDeskReservation(deskReservation);
            }

        }
    }
    public enum DeskStatus
    {
        Available = 0,
        UnAvailable = 1,
        BookedByMe = 2,
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
