using System;
using System.Collections.Generic;

namespace BudgetTracker.Models
{
    public partial class Expense
    {
        public Expense()
        {
            //this.BudgetCategories = new List<BudgetCategory>();
        }

        public System.Guid id { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        //public virtual ICollection<BudgetCategory> BudgetCategories { get; set; }
    }
}
