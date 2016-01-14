using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FFC.Framework.Data;
using FFC.Framework.ClientSubscription.Web.Models;

namespace FFC.Framework.ClientSubscription.Web.Controllers
{
    public class ForecastFailoverController : Controller
    {
        private FFCEntities db = new FFCEntities();

        //
        // GET: /ForecastFailover/

        public ActionResult Index()
        {

            ForecastFailoverModel ffModel = new ForecastFailoverModel();
            List<BranchItemData> branchItemDataList = new List<BranchItemData>();

            foreach (var branch in db.Branches.ToList())
            {
                BranchItemData branchItemData = new BranchItemData();
                branchItemData.Branch = branch;
                branchItemDataList.Add(branchItemData);
            }

            ffModel.BranchItemDataList = branchItemDataList;
            return View(ffModel);
        }

        [HttpPost]
        public ActionResult Index(ForecastFailoverModel model)
        {

            model.ForecastFailoverResult = "This is the forecast failover result";

            return View("Index", model);
        }

        //
        // GET: /ForecastFailover/Details/5

        public ActionResult Details(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        //
        // GET: /ForecastFailover/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ForecastFailover/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Branches.Add(branch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(branch);
        }

        //
        // GET: /ForecastFailover/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        //
        // POST: /ForecastFailover/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Branch branch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        //
        // GET: /ForecastFailover/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Branch branch = db.Branches.Find(id);
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        //
        // POST: /ForecastFailover/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Branch branch = db.Branches.Find(id);
            db.Branches.Remove(branch);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}