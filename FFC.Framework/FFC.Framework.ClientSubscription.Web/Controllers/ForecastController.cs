using FFC.Framework.ClientSubscription.Web.Models;
using FFC.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFC.Framework.ClientSubscription.Web.Controllers
{
    public class ForecastController : Controller
    {
        //
        // GET: /Forecast/

        public ActionResult Index()
        {
            //ForecastModel forecastModel = new ForecastModel();

            List<string> list = Enum.GetNames(typeof(Methods)).ToList();
            ViewBag.ForecastMethods = new SelectList(list.Select(x => new { Value = x, Text = x }),"Value","Text");
            
            return View();
        }

        //
        // GET: /Forecast/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Forecast/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Forecast/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Forecast/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Forecast/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Forecast/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Forecast/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
