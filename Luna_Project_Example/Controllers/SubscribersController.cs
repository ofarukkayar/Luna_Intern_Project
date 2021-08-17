using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Luna_Project_Example.Data.ApplicationDbContext;
using Luna_Project_Example.Models;

namespace Luna_Project_Example.Controllers
{
    public class SubscribersController : Controller
    {
        public cs db = new cs();

        // GET: Subscribers
        public ActionResult Index()
        {
            
            return View(db.Subscribers.ToList());
        }

        // GET: Subscribers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscriber subscriber = db.Subscribers.Find(id);
            if (subscriber == null)
            {
                return HttpNotFound();
            }
            return View(subscriber);
        }

        // GET: Subscribers/Create
        public ActionResult Create()
        {
            List<int> list = new List<int>();
            foreach (var item in db.Meters)
            {
                int temp = item.meterID;
                list.Add(temp);
            }
            ViewBag.list = list;
            return View();
        }

        // POST: Subscribers/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "subID,name,surname,meter_meterID")] Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                Meter meter = db.Meters.Find(subscriber.meter_meterID);
                if (meter == null || meter.numberOfSubs < meter.maxNumberOfSubs)
                {
                    if(meter != null)
                        meter.numberOfSubs++;
                    db.Subscribers.Add(subscriber);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else 
                {
                    Content("<script language='javascript' type='text/javascript'>alert('The maximum amount for this meter is reached');</script>");
                    return RedirectToAction("Index");
                }
                    

            }

            return View(subscriber);
        }

        // GET: Subscribers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscriber subscriber = db.Subscribers.Find(id);

            List<int> list = new List<int>();
            foreach (var item in db.Meters)
            {
                int temp = item.meterID;
                list.Add(temp);
            }
            ViewBag.list = list;

            if (subscriber == null)
            {
                return HttpNotFound();
            }
            return View(subscriber);
        }

        // POST: Subscribers/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "subID,name,surname,meter_meterID")] Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                Meter meter = db.Meters.Find(subscriber.meter_meterID);

                if (meter.numberOfSubs < meter.maxNumberOfSubs)
                {
                    Subscriber unedittedSub = db.Subscribers.AsNoTracking().FirstOrDefault(i => i.subID == subscriber.subID);
                    if(unedittedSub.meter_meterID != meter.meterID)
                    {
                        meter.numberOfSubs++;
                        Meter oldMeter = db.Meters.Find(unedittedSub.meter_meterID);
                        if(oldMeter != null)
                            oldMeter.numberOfSubs--;
                    }
                    db.Entry(meter).State = EntityState.Modified;
                    db.Entry(subscriber).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('The maximum amount for this meter is reached');</script>");
                }
            }
            return View(subscriber);
        }

        // GET: Subscribers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscriber subscriber = db.Subscribers.Find(id);
            if (subscriber == null)
            {
                return HttpNotFound();
            }
            return View(subscriber);
        }

        // POST: Subscribers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscriber subscriber = db.Subscribers.Find(id);
            db.Subscribers.Remove(subscriber);
            if (db.Meters.Find(subscriber.meter_meterID) != null)
            {
                Meter meter = db.Meters.Find(subscriber.meter_meterID);
                if (meter.numberOfSubs > 0)
                    meter.numberOfSubs--;
                else
                    Content("<script language='javascript' type='text/javascript'>alert('Deletion had some problems with the Meter Subscription');</script>");
            }
            else
                Content("<script language='javascript' type='text/javascript'>alert('Deletion had some problems with the Meter Subscription');</script>");
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
