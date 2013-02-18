using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BudgetTracker.Models;

namespace BudgetTracker.Controllers
{
    public class CategoryAPIController : ApiController
    {
        private BudgetTrackerContext db = new BudgetTrackerContext();

        // GET api/CategoryAPI
        public IEnumerable<BudgetCategory> GetBudgetCategories()
        {
            return db.BudgetCategories.AsEnumerable();
        }

        // GET api/CategoryAPI/5
        public BudgetCategory GetBudgetCategory(Guid id)
        {
            BudgetCategory budgetcategory = db.BudgetCategories.Find(id);
            if (budgetcategory == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return budgetcategory;
        }

        // PUT api/CategoryAPI/5
        public HttpResponseMessage PutBudgetCategory(Guid id, BudgetCategory budgetcategory)
        {
            if (ModelState.IsValid && id == budgetcategory.id)
            {
                db.Entry(budgetcategory).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/CategoryAPI
        public HttpResponseMessage PostBudgetCategory(BudgetCategory budgetcategory)
        {
            budgetcategory.id = System.Guid.NewGuid();
            if (ModelState.IsValid)
            {
                db.BudgetCategories.Add(budgetcategory);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, budgetcategory);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = budgetcategory.id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/CategoryAPI/5
        public HttpResponseMessage DeleteBudgetCategory(Guid id)
        {
            BudgetCategory budgetcategory = db.BudgetCategories.Find(id);
            if (budgetcategory == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.BudgetCategories.Remove(budgetcategory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, budgetcategory);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}