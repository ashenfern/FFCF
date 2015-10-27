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
        public DateTime StartDate { get; set; }
        public Methods Method { get; set; }
        public DataPeriod dataPeriod { get; set; }
        public int period { get; set; }
    }

}
