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
    public class ExpenseAPIController : ApiController
    {
        private Models.Entities db = new Models.Entities();
        

        // GET api/ExpenseAPI
        public IEnumerable<Expenses> GetExpenses()
        {
            return db.Expenses.AsEnumerable();
        }

        // GET api/ExpenseAPI/5
        public Expenses GetExpense(Guid id)
        {
            Expenses expense = db.Expenses.Find(id);
            if (expense == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return expense;
        }

        // PUT api/ExpenseAPI/5
        public HttpResponseMessage PutExpense(Guid id, Expenses expense)
        {
            if (ModelState.IsValid && id == expense.id)
            {
                db.Entry(expense).State = EntityState.Modified;

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

        // POST api/ExpenseAPI
        public HttpResponseMessage PostExpense(Expenses expense)
        {
            expense.id = System.Guid.NewGuid();
            if (ModelState.IsValid)
            {
                db.Expenses.Add(expense);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, expense);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = expense.id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/ExpenseAPI/5
        public HttpResponseMessage DeleteExpense(Guid id)
        {
            Expenses expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Expenses.Remove(expense);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, expense);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}