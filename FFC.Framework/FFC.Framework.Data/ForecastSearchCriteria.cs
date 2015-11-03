using FFC.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.Data
{
    public class ForecastSearchCriteria
    {
        public int BranchId { get; set; }
        public int ProductId { get; set; }
        public Forecast_Methods Method { get; set; }
        public Forecast_DatePeriods DatePeriod { get; set; }
        public int ForecastPeriod { get; set; }
        public DateTime StartDate { get; set; }
    }

}
