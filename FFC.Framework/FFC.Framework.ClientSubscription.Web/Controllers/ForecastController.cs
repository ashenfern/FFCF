﻿using FFC.Framework.ClientSubscription.Business;
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
        FFCEntities db = new FFCEntities();

        //
        // GET: /Forecast/

        public ActionResult Index()
        {
            //ForecastModel forecastModel = new ForecastModel();

            //List<string> listMethods = Enum.GetNames(typeof(Methods)).ToList();
            //List<string> listDateTypes = Enum.GetNames(typeof(DataPeriod)).ToList();

            ViewBag.Branches = new SelectList(db.Branches, "BranchID", "BranchName");
            ViewBag.Products = new SelectList(db.Products, "ProductID", "ProductName");
            ViewBag.ForecastMethods = new SelectList(db.Forecast_Methods, "ForecastIdentifier", "ForecastMethod");
            ViewBag.DateTypes = new SelectList(db.Forecast_DatePeriods, "DatePeriod", "DatePeriod");

            return View();
        }

        //
        // POST: /Forecast/Index

        [HttpPost]
        public ActionResult Index(ForecastModel model)
        {
            try
            {
                
                ViewBag.Branches = new SelectList(db.Branches, "BranchID", "BranchName");
                ViewBag.Products = new SelectList(db.Products, "ProductID", "ProductName");
                ViewBag.ForecastMethods = new SelectList(db.Forecast_Methods, "ForecastIdentifier", "ForecastMethod");
                ViewBag.DateTypes = new SelectList(db.Forecast_DatePeriods, "DatePeriod", "DatePeriod");

                ForecastBusinessManger fcastManager = new ForecastBusinessManger();

                ForecastSearchCriteria fcastSearchCriteria = new ForecastSearchCriteria();
                fcastSearchCriteria.BranchId = model.ForecastSearchCriteria.BranchId;
                fcastSearchCriteria.ProductId = model.ForecastSearchCriteria.ProductId;
                fcastSearchCriteria.Method = new Forecast_Methods() { ForecastIdentifier = model.ForecastSearchCriteria.Method.ForecastIdentifier };
                fcastSearchCriteria.DatePeriod = new Forecast_DatePeriods(){DatePeriod = model.ForecastSearchCriteria.DatePeriod.DatePeriod};
                fcastSearchCriteria.ForecastPeriod = model.ForecastSearchCriteria.ForecastPeriod;
                
                var result = fcastManager.GetForecastResults(fcastSearchCriteria);

                model.ForecastResult = result;

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
