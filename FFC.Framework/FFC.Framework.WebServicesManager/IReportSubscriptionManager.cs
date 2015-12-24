using FFC.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.WebServicesManager
{
    interface IReportSubscriptionManager 
    {
        bool CreateFileSubscription(ReportSchedule reportSchedule);
        bool CreateEmailSubscription(ReportSchedule reportSchedule);
        bool IsSubscriptionExist(int reportId);
        List<ReportSchedule> GetSubscriptions(int reportId);
        void UpdateSubscription(ReportSchedule reportSchedule);
        void DeleteSubscription(int id);


    }
}
