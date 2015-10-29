using FFC.Framework.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFC.Framework.ClientSubscription.Web.Controllers
{
    public class TransactionDetailController : Controller
    {
        private FFCEntities db = new FFCEntities();

        //
        // GET: /TransactionDetail/

        public ActionResult Index()
        {
            var transactiondetails = db.TransactionDetails.Include(t => t.Product).Include(t => t.Transaction);
            return View(transactiondetails.ToList());
        }

        //
        // GET: /TransactionDetail/Details/5

        public ActionResult Details(int id = 0)
        {
            TransactionDetail transactiondetail = db.TransactionDetails.Find(id);
            if (transactiondetail == null)
            {
                return HttpNotFound();
            }
            return View(transactiondetail);
        }

        //
        // GET: /TransactionDetail/Create

        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName");
            ViewBag.TransactionID = new SelectList(db.Transactions, "TransactionID", "TransactionID");
            return View();
        }

        //
        // POST: /TransactionDetail/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionDetail transactiondetail)
        {
            if (ModelState.IsValid)
            {
                db.TransactionDetails.Add(transactiondetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", transactiondetail.ProductID);
            ViewBag.TransactionID = new SelectList(db.Transactions, "TransactionID", "TransactionID", transactiondetail.TransactionID);
            return View(transactiondetail);
        }

        //
        // GET: /TransactionDetail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TransactionDetail transactiondetail = db.TransactionDetails.Find(id);
            if (transactiondetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", transactiondetail.ProductID);
            ViewBag.TransactionID = new SelectList(db.Transactions, "TransactionID", "TransactionID", transactiondetail.TransactionID);
            return View(transactiondetail);
        }

        //
        // POST: /TransactionDetail/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TransactionDetail transactiondetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactiondetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", transactiondetail.ProductID);
            ViewBag.TransactionID = new SelectList(db.Transactions, "TransactionID", "TransactionID", transactiondetail.TransactionID);
            return View(transactiondetail);
        }

        //
        // GET: /TransactionDetail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TransactionDetail transactiondetail = db.TransactionDetails.Find(id);
            if (transactiondetail == null)
            {
                return HttpNotFound();
            }
            return View(transactiondetail);
        }

        //
        // POST: /TransactionDetail/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransactionDetail transactiondetail = db.TransactionDetails.Find(id);
            db.TransactionDetails.Remove(transactiondetail);
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