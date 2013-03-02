using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BudgetTracker.Models
{
    [KnownType(typeof(BudgetCategory))]
    public partial class BudgetCategory
    {
        public BudgetCategory()
        {
            //this.Expenses = new List<Expense>();
        }

        public System.Guid id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Type { get; set; }
        //public virtual ICollection<Expense> Expenses { get; set; }
    }
}
