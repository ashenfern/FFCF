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
    public class ForecastFailoverController : ApiController
    {
        private ForecastFailoverManager ffManager = new ForecastFailoverManager();

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post(List<BranchItemData> branchItemDataList)
        {
            string ffResult = ffManager.GetForecastFailoverResultsFromAlgorithm(branchItemDataList);
            var response = Request.CreateResponse(HttpStatusCode.Created, ffResult);
            return response;
        }

        #region Previous
        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //} 
        #endregion
    }
}