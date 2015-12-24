using FFC.Framework.Common;
using FFC.Framework.Data;
using RDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.WebServicesManager
{
    interface IForecastManager
    {
         ForecastResult ForecastByMethod(int branchId, int productId, string method, string dataType, int periods);
         ForecastResult ForecastByMethod(int branchId, List<int> productIdList, string method, string dataType, int periods);
         string PlotForecast(REngine engine, string method);
         List<double> GetCorrespondingDataByPeriod(DataPeriod period, int productId);

    }
}
