using HotDesk.Models;
using System;
using System.Linq;

namespace HotDesk.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HotDeskContext context)
        {
            // Look for any desks.
            if (context.Desk.Any())
            {
                return;   // DB has been seeded
            }

            var desks = new Desk[]
            {
                new Desk{Name="Desk 1",Location="Floor 1 Row 1",Category="A"},
                new Desk{Name="Desk 2",Location="Floor 1 Row 2",Category="A"},
                new Desk{Name="Desk 3",Location="Floor 2 Row 1",Category="B"}
            };

            context.Desk.AddRange(desks);
            context.SaveChanges();

            var resvdates = new ResvDate[]
            {
                new ResvDate{Name="Jun", FromDate=DateTime.Parse("2021-07-01"),ToDate=DateTime.Parse("2021-07-30")},
                new ResvDate{Name="Jan", FromDate=DateTime.Parse("2021-01-01"),ToDate=DateTime.Parse("2021-07-31")},
                new ResvDate{Name="Feb", FromDate=DateTime.Parse("2021-02-01"),ToDate=DateTime.Parse("2021-02-28")}
            };

            context.ResvDate.AddRange(resvdates);
            context.SaveChanges();

            var availdesks = new AvailDesk[]
            {
                new AvailDesk{DeskID=1,ResvDateID=1},
                new AvailDesk{DeskID=1,ResvDateID=2},
                new AvailDesk{DeskID=2,ResvDateID=3},
                new AvailDesk{DeskID=3,ResvDateID=1}
            };

            context.AvailDesk.AddRange(availdesks);
            context.SaveChanges();
        }
    }
}