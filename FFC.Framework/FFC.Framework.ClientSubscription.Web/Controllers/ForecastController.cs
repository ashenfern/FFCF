using FFC.Framework.ClientSubscription.Web.Models;
using FFC.Framework.Common;
using FFC.Framework.Data;
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

            List<string> listMethods = Enum.GetNames(typeof(Methods)).ToList();
            List<string> listDateTypes = Enum.GetNames(typeof(DataPeriod)).ToList();

            ViewBag.ForecastMethods = new SelectList(listMethods.Select(x => new { Value = x, Text = x }), "Value", "Text");
            ViewBag.DateTypes = new SelectList(listDateTypes.Select(x => new { Value = x, Text = x }), "Value", "Text");

            return View();
        }

        //
        // POST: /Forecast/Index

        [HttpPost]
        public ActionResult Index(ForecastModel model)
        {
            try
            {
                ViewBag.ForecastMethods = new SelectList(model.ForecastMethods.Select(x => new { Value = x, Text = x }), "Value", "Text");
                ViewBag.DateTypes = new SelectList(model.DateTypes.Select(x => new { Value = x, Text = x }), "Value", "Text");
                  
                //TODO: Calling the web api and get the result
                ForecastResult forecastResult = new ForecastResult() { Method = Methods.Arima, results = new List<double>() { 1.0 } };
                model.ForecastResult = forecastResult;

                //return RedirectToAction("Index");
                return View("Index", model);
            }
            catch
            {
                return View();
            }
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
