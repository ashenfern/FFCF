using FFC.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFC.Framework.ClientSubscription.Web.Models
{
    public class ForecastModel
    {
        //public Forecast_Methods ForecastMethod { get; set; }
        //public Forecast_DatePeriods ForecastDateType { get; set; }
        public ForecastSearchCriteria ForecastSearchCriteria { get; set; }
        public ForecastResult ForecastResult { get; set; }
    }
}