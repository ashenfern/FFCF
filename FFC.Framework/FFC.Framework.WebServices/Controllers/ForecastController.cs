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
    public class ForecastController : ApiController
    {

        private FFCEntities db = new FFCEntities();
        private ForecastManager fm = new ForecastManager();
        //private RManager rm = new RManager();

        // GET 
        //[Route("Forecast/{customerId}/orders/{orderId}")]
        [ActionName("ByProductDayTime")]
        public List<sp_Forecast_GetDailyTimeSpecificAvereageProductTransactions_Result> GetProducts(int day, int productId, int start, int end)
        {
            var result = fm.GetDailyTimeSpecificAvereageProductTransactions().Where(r => r.Day == day).ToList();
            return result;
        }

        [ActionName("TestForecast")]
        [System.Web.Http.HttpGet]
        public ForecastResult ForecastTest(int id)
        {
            var result = ForecastManager.fcast2();
            return result;
        }

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
    }
}