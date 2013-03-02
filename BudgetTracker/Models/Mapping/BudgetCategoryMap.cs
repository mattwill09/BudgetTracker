using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BudgetTracker.Models.Mapping
{
    public class BudgetCategoryMap : EntityTypeConfiguration<BudgetCategory>
    {
        public BudgetCategoryMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("BudgetCategories");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Type).HasColumnName("Type");

            // Relationships
            //this.HasMany(t => t.Expenses)
            //    .WithMany(t => t.BudgetCategories)
            //    .Map(m =>
            //        {
            //            m.ToTable("ExpenseBudgetCategories");
            //            m.MapLeftKey("BudgetCategory_id");
            //            m.MapRightKey("Expense_id");
            //        });


        }
    }
}
