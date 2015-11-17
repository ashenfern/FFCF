using FFC.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.ClientSubscription.Business
{
    public class ReportBusinessManager
    {
        WebServiceConnector<ReportSchedule> service = new WebServiceConnector<ReportSchedule>();

        public ReportSchedule GetReportSubscriptions()
        {
            service.Resource = "api/Report/1/";
            var result = service.GetData();
            
            return result;
        }

        public bool CreateSubscription(ReportSchedule reportSchedule)
        {
            service.Resource = "api/Report/";
            service.CreateResult(reportSchedule);

            return true;
        }
    }
}
