using FFC.Framework.ClientSubscription.ServiceFacade;
using FFC.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.ClientSubscription.Business
{
    public class ForecastFailoverBusinessManger
    {
        public String GetForecastResults(List<BranchItemData> listBranchItemData)
        {
            return new ForecastFailoverServiceFacade().GetForecastFailoverResults(listBranchItemData);
        }
    }
}
