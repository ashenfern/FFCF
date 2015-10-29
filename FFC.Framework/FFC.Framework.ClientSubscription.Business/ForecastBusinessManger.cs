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

        public ForecastResult GetForecastResults()
        {
            service.Resource = "api/Forecast/TestForecast/1";
            var result = service.GetData();
            return result;
        }
    }
}
