using FFC.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.ClientSubscription.ServiceFacade
{
    public class ForecastFailoverServiceFacade
    {
        WebServiceGenericConnector<List<BranchItemData>> service = new WebServiceGenericConnector<List<BranchItemData>>();
        public string GetForecastFailoverResults(List<BranchItemData> listBranchItemData)
        {
            service.Resource = "api/ForecastFailover/";
            string result = service.PostFFData(listBranchItemData);

            return result;
        }
    }
}
