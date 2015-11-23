using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFC.Framework.Data;
using FFC.Framework.ClientSubscription.Business;
using FFC.Framework.ClientSubscription.Web.Models;

namespace FFC.Framework.ClientSubscription.Web.Controllers
{
    public class ReportController : Controller
    {
        private FFCEntities db = new FFCEntities();
        private ReportBusinessManager reportBusinessManager = new ReportBusinessManager();

        //
        // GET: /Report/

        public ActionResult Index()
        {
            //Getting report schedules from the report DB
            //var subscriptions = reportBusinessManager.GetSubscriptions();

            //foreach (var subscription in subscriptions)
            //{
            //    //subscription.
            //}

            //var reportschedules = db.ReportSchedules.Include(r => r.Report);
            ViewBag.Reports = new SelectList(db.Reports, "ReportId", "ReportName");
            //ViewBag.ReportId = new SelectList(db.Reports, "ReportId", "ReportName");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(ReportModel model)
        {
            ViewBag.ReportId = new SelectList(db.Reports, "ReportId", "ReportName");
            //model.ResultSchedules = db.ReportSchedules.Include(r => r.Report).Where(r => r.ReportId == model.ReportId).ToList();
            //var reportName = db.Reports.Where(r => r.ReportId == model.ReportId).Select(r=>r.ReportName).FirstOrDefault();

            model.ResultSchedules = reportBusinessManager.GetReportSubscriptions(model.ReportId);
            return View(model);
        }

        //
        // GET: /Report/Details/5

        public ActionResult Details(int id = 0)
        {
            ReportSchedule reportschedule = db.ReportSchedules.Find(id);
            if (reportschedule == null)
            {
                return HttpNotFound();
            }
            return View(reportschedule);
        }

        //
        // GET: /Report/Create

        public ActionResult Create()
        {
            ViewBag.ReportId = new SelectList(db.Reports, "ReportId", "ReportName");

            ViewBag.SchedulePeriod = GetDropdownDataByName("SchedulePeriod");
            ViewBag.ScheduleDay = GetDropdownDataByName("ScheduleDay");
            ViewBag.DeliveryTypeId = new SelectList(db.FileDeliveryTypes, "DeliveryTypeId", "DeliveryMethod");

            return View();
        }

        //
        // POST: /Report/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReportSchedule reportschedule)
        {
            reportschedule.Report = db.Reports.Where(r => r.ReportId == reportschedule.ReportId).FirstOrDefault();
            var result = reportBusinessManager.CreateSubscription(reportschedule);

            //if (ModelState.IsValid)
            //{
                //db.ReportSchedules.Add(reportschedule);
                //db.SaveChanges();
                //return RedirectToAction("Index");
            //}

            ViewBag.ReportId = new SelectList(db.Reports, "ReportId", "ReportName", reportschedule.ReportId);
            
            return RedirectToAction("Index");
        }

        //
        // GET: /Report/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ReportSchedule reportschedule = db.ReportSchedules.Find(id);
            if (reportschedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReportId = new SelectList(db.Reports, "ReportId", "ReportName", reportschedule.ReportId);

            ViewBag.SchedulePeriod = new SelectList(GetDropdownDataByName("SchedulePeriod"), "Value", "Text", reportschedule.SchedulePeriod);
            ViewBag.ScheduleDay = new SelectList(GetDropdownDataByName("ScheduleDay"), "Value", "Text", reportschedule.ScheduleDay);
            ViewBag.DeliveryTypeId = new SelectList(db.FileDeliveryTypes, "DeliveryTypeId", "DeliveryMethod", reportschedule.DeliveryTypeId);
            return View(reportschedule);
        }

        //
        // POST: /Report/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReportSchedule reportschedule)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(reportschedule).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.ReportId = new SelectList(db.Reports, "ReportId", "ReportName", reportschedule.ReportId);

           // reportschedule.ReportSubscriptionId = db.ReportSchedules.Where(r => r.ReportId == reportschedule.ReportId).Select(r => r.ReportSubscriptionId).FirstOrDefault();

            reportBusinessManager.EditSubscription(reportschedule);
            ViewBag.ReportId = new SelectList(db.Reports, "ReportId", "ReportName");
            return RedirectToAction("Index");
        }

        //
        // GET: /Report/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ReportSchedule reportschedule = db.ReportSchedules.Find(id);
            if (reportschedule == null)
            {
                return HttpNotFound();
            }
            return View(reportschedule);
        }

        //
        // POST: /Report/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            reportBusinessManager.DeleteSubscription(id);

            //ReportSchedule reportschedule = db.ReportSchedules.Find(id);
            //db.ReportSchedules.Remove(reportschedule);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public List<SelectListItem> GetDropdownDataByName(string name)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            if (name == "SchedulePeriod")
            {
              selectList.Add(new SelectListItem {Text = "Daily", Value = "Daily"});
              selectList.Add(new SelectListItem {Text = "Weekly", Value = "Weekly"});
              selectList.Add(new SelectListItem {Text = "Monthly", Value = "Monthly"});
              selectList.Add(new SelectListItem {Text = "Yearly", Value = "Yearly"});
            }
            else if (name == "ScheduleDay")
            {
               selectList.Add(new SelectListItem {Text = "Sunday", Value = "0"});
               selectList.Add(new SelectListItem {Text = "Monday", Value = "1"});
               selectList.Add(new SelectListItem {Text = "Tuesday", Value = "2"});
               selectList.Add(new SelectListItem {Text = "Wednesday", Value = "3"});
               selectList.Add(new SelectListItem {Text = "Thurseday", Value = "4"});
               selectList.Add(new SelectListItem {Text = "Friday", Value = "5"});
               selectList.Add(new SelectListItem { Text = "Saturday", Value = "6" });
            }

            
            return selectList;
        }

    }
}