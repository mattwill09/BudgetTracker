using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using BudgetTracker.Models.Mapping;

namespace BudgetTracker.Models
{
    public partial class BudgetTrackerContext : DbContext
    {
        static BudgetTrackerContext()
        {
            Database.SetInitializer<BudgetTrackerContext>(null);
        }

        public BudgetTrackerContext()
            : base("Name=BudgetTrackerContext")
        {
        }

        public DbSet<BudgetCategory> BudgetCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BudgetCategoryMap());
            modelBuilder.Configurations.Add(new ExpenseMap());
        }
    }
}
