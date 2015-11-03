using FFC.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.Data
{
    public class ForecastResult
    {
        public int BranchId { get; set; }
        public int ProductId { get; set; }
        public string Method { get; set; }
        public string DatePeriod { get; set; }
        public int ForecastPeriod { get; set; }
        public List<double> Values { get; set; }
        public string ImagePath { get; set; }
    }
}
