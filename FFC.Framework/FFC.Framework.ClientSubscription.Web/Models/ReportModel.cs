using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FFC.Framework.Data;

namespace FFC.Framework.ClientSubscription.Web.Models
{
    public class ReportModel
    {
        public Report Report { get; set; }
        public int ReportId { get; set; }
        public List<ReportSchedule> ResultSchedules { get; set; }
    }
}