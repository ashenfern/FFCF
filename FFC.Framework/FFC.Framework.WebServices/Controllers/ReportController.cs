using FFC.Framework.Data;
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

        #region Default Methods
        // GET api/report
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/report/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/report
        public void Post(ReportSchedule value)
        {
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
