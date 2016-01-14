using FFC.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FFC.Framework.ClientSubscription.Web.Models
{
    public class ForecastFailoverModel
    {
        public List<BranchItemData> BranchItemDataList { get; set; }
        public string ForecastFailoverResult { get; set; }
    }
}