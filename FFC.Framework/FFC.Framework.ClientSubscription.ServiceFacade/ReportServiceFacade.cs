using FFC.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.ClientSubscription.ServiceFacade
{
    public static class ReportServiceFacade
    {
        WebServiceGenericConnector<ReportSchedule> service = new WebServiceGenericConnector<ReportSchedule>();

        public List<ReportSchedule> GetReportSubscriptions(int reportId)
        {
            service.Resource = "api/Report/GetByName/";
            var result = service.GetDataListById(reportId);

            return result;
        }

        public bool CreateSubscription(ReportSchedule reportSchedule)
        {
            service.Resource = "api/Report/";
            service.PostData(reportSchedule);

            return true;
        }

        public bool EditSubscription(ReportSchedule reportSchedule)
        {
            service.Resource = "api/Report/";
            service.PutData(reportSchedule);

            return true;
        }

        public bool DeleteSubscription(int id)
        {
            service.Resource = "api/Report/";
            service.DeleteResult(id);

            return true;
        }
    }
}

