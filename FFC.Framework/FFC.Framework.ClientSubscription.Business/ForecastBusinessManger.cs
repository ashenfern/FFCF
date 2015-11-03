using FFC.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.ClientSubscription.Business
{
    public class ForecastBusinessManger
    {
        WebServiceConnector<ForecastResult> service = new WebServiceConnector<ForecastResult>();

        public ForecastResult GetForecastResults(ForecastSearchCriteria searchCriteria)
        {
            int branchId = searchCriteria.BranchId;
            int productId = searchCriteria.ProductId;
            string method = searchCriteria.Method.ForecastIdentifier.ToString();
            string dataType = searchCriteria.DatePeriod.DatePeriod.ToString();
            int periods = searchCriteria.ForecastPeriod;

            //service.Resource = "api/Forecast/ForecastByMethod/1";
            service.Resource = String.Format("api/Forecast/ForecastByMethod/{0}/{1}/{2}/{3}/{4}", branchId,productId, method, dataType, periods);
            var result = service.GetData();
            return result;
        }
    }
}
