//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestConsole
{
    using System;
    using System.Collections.Generic;
    
    public partial class Report
    {
        public Report()
        {
            this.ReportSchedules = new HashSet<ReportSchedule>();
        }
    
        public int ReportId { get; set; }
        public string ReportName { get; set; }
        public string ReportDescription { get; set; }
        public string ReportPath { get; set; }
        public string ReportFileName { get; set; }
    
        public virtual ICollection<ReportSchedule> ReportSchedules { get; set; }
    }
}
