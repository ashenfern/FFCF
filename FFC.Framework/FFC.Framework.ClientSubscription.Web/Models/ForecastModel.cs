using FFC.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFC.Framework.ClientSubscription.Web.Models
{
    public class ForecastModel
    {
        public List<string> ForecastMethods { get; set; }
        public List<string> DateTypes { get; set; }
        public ForecastResult ForecastResult { get; set; }
    }
}