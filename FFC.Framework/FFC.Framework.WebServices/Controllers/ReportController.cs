using FFC.Framework.Data;
using FFC.Framework.WebServicesManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FFC.Framework.WebServices.Controllers
{
    public class ReportController : ApiController
    {

        private ReportSubscriptionManager  reportManager = new ReportSubscriptionManager();

        public void Get()
        {
           //var result = reportManager.GetSubscriptions("ReportName");
           // return result;
            //return new string[] { "value1", "value2" };
        }

        #region Default Methods
        // GET api/report
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/report/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/report
        //[ActionName("GetSubscriptions")]
        //public List<ReportSchedule> Post(ReportSchedule value)
        //{
        //    var result = reportManager.GetSubscriptions(value);

        //    List<ReportSchedule> listReportSchedule = new List<ReportSchedule>();
        //    return listReportSchedule;
        //}

        [ActionName("CreateSubscriptions")]
        public void Post(ReportSchedule reportSchedule)
        {
            var result = reportManager.CreateSubscription(reportSchedule, @"\\CMLASHFERNANDO\Shared");
        }

        // PUT api/report/5

        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/report/5
        public void Delete(int id)
        {
        } 
        #endregion
    }
}
