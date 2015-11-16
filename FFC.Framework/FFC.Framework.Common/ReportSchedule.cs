using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.Common
{
    public class ReportSchedule
    {
        public string Report_Name_Code { get; set; }
        public string Report_Schedule_Type_Code { get; set; }
        public string RDL_Path { get; set; }
        public string RDL_Filename { get; set; }
        public int Report_Schedule_Day { get; set; }
        public System.TimeSpan Time_to_Execute_the_Report { get; set; }
        public int Schedule_ID { get; set; }
        public Nullable<System.DateTime> Updated_Date { get; set; }
        public Nullable<System.DateTime> Last_Synchronization_Date { get; set; }
        public string Report_Name_Description { get; set; }
        public System.DateTime Expiration_Date_for_the_Schedule { get; set; }
        public int Hierarchy_ID { get; set; }
        public Nullable<int> Release_ID { get; set; }
        public Nullable<int> Deployment_ID { get; set; }
    }
}
