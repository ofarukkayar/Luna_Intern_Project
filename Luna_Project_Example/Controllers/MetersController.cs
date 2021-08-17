using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Luna_Project_Example.Data.ApplicationDbContext;
using Luna_Project_Example.Models;

namespace Luna_Project_Example.Controllers
{
    public class MetersController : Controller
    {
        public cs db = new cs();
        public int? curID;

        // GET: Meters
        public ActionResult Index(int? id)
        {
            
            if (id != null)
                curID = id.Value;
            else
                curID = null;
            return View(db.Meters.ToList());
        }

        // GET: Meters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meter meter = db.Meters.Find(id);
            if (meter == null)
            {
                return HttpNotFound();
            }
            return View(meter);
        }

        // GET: Meters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Meters/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "serialId,valveWidth,maxNumberOfSubs,productionDate,batteryState")] Meter meter)
        {
            
            if (ModelState.IsValid)
            {
                db.Meters.Add(meter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(meter);
        }


        
        
        public ActionResult Associate(int? id)
        {
            if (ModelState.IsValid)
            {
                return View(db.Subscribers.Where(i => i.meter_meterID == id).ToList());
            }
            return RedirectToAction("Index");
        }

        
        public ActionResult Unsubscribe(int? id)
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
        [HttpPost, ActionName("Unsubscribe")]
        [ValidateAntiForgeryToken]
        public ActionResult UnsubscribeConfirmed(int? id)
        {
            Subscriber entityItem = db.Subscribers.First(s => s.subID == id.Value);
            Meter meter = db.Meters.Find(entityItem.meter_meterID);
            entityItem.meter_meterID = null;
            meter.numberOfSubs--;

            db.Entry(entityItem).State = EntityState.Modified;
            db.Entry(meter).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Meters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meter meter = db.Meters.Find(id);
            if (meter == null)
            {
                return HttpNotFound();
            }
            return View(meter);
        }

        // POST: Meters/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "meterID,valveWidth,numberOfSubs,maxNumberOfSubs,productionDate,batteryState")] Meter meter)
        {
            if (ModelState.IsValid)
            {
                Meter oldMeter = db.Meters.Find(meter.meterID);
                if (oldMeter.numberOfSubs <= meter.maxNumberOfSubs)
                {
                    db.Meters.Remove(oldMeter);
                    db.Meters.Add(meter);
                    db.SaveChanges();
                }
                else
                    Content("<script language='javascript' type='text/javascript'>alert('The maximum amount cannot be reduced due to ongoing subs');</script>");
                return RedirectToAction("Index");
            }
            return View(meter);
        }

        // GET: Meters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meter meter = db.Meters.Find(id);
            if (meter == null)
            {
                return HttpNotFound();
            }
            return View(meter);
        }

        // POST: Meters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meter meter = db.Meters.Find(id);
            db.Meters.Remove(meter);
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
