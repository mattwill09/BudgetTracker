﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BudgetTracker.Controllers
{
    public class ExpenseController : Controller
    {
        //
        // GET: /Expense/

        public ActionResult Index()
        {
            return View();
        }

        
    }
}
