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

        public List<ReportSchedule> GetReportSubscriptions()
        {
            service.Resource = "api/Report/";
            var result = service.GetDataList();
            
            return result;
        }

        public bool CreateSubscription(ReportSchedule reportSchedule)
        {
            service.Resource = "api/Report/";
            service.PutData(reportSchedule);

            return true;
        }
    }
}
