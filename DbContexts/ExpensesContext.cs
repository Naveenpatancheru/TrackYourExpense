using Microsoft.EntityFrameworkCore;
using TrackYourExpenseApi.Entities;
using System;

namespace TrackYourExpenseApi.DbContexts
{
    public class ExpensesContext : DbContext
    {
       public ExpensesContext(DbContextOptions<ExpensesContext> options) : base(options)
       {

       }
       public DbSet<Expense> Expenses { get; set; }
       public DbSet<Transaction> Transacations { get; set; }
       public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Expense>().HasData(
                new Expense()
                {
                    Id = Guid.NewGuid(),
                    Type = "Rent",
                    Description = "Monthly Rent Expense"
                },
                new Expense()
                {
                    Id = Guid.NewGuid(),
                    Type = "Miscellaneous",
                    Description = "Monthly Rent Expense"
                });

             modelBuilder.Entity<Transaction>().HasData(
                new Transaction()
                {
                    Id = Guid.NewGuid(),
                    Text = "Car Rent",
                    Description = "Monthly Car Rental Expenses"
                },
                new Transaction()
                {
                    Id = Guid.NewGuid(),
                    Text = "Miscellaneous",
                    Description = "Monthly Rent Expense"
                });
        }
    }
}
