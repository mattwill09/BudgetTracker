//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BudgetTracker.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BudgetCategory
    {
        public BudgetCategory()
        {
            this.Expenses = new HashSet<Expense>();
        }
    
        public System.Guid id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public string Type { get; set; }
    
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
