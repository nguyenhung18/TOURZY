using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class DashboardDTO
    {
        public int CustomerCount { get; set; }
        public int TourCount { get; set; }
        public string DailyRevenue { get; set; }
        public string MonthlyRevenue { get; set; }
    }
}
