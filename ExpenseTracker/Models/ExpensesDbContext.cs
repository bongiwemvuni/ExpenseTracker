using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Models

{
    public class ExpensesDbContext : DbContext
    {
       public  DbSet<Expense> _expenses { get; set; }
         
        public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options)
            : base(options)
        {
              
        }
    }
}
