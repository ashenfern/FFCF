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
        WebServiceGenericConnector<String> service = new WebServiceGenericConnector<String>();
        public string GetForecastFailoverResults(List<BranchItemData> listBranchItemData)
        {
            service.Resource = "api/ForecastFailover/";
            //service.PostData(listBranchItemData);

            return String.Empty;
        }
    }
}
