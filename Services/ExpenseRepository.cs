using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackYourExpenseApi.DbContexts;
using TrackYourExpenseApi.Entities;

namespace TrackYourExpenseApi.Services
{
    public class ExpenseRepository : IExpenseRepository, IDisposable
    {
        public ExpensesContext _context;

        public ExpenseRepository(ExpensesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
        }
     
        public  async Task<Expense> GetExpenseAsync(Guid id)
        {
            return await _context.Expenses.FirstOrDefaultAsync(b => b.Id == id);

        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            return await _context.Expenses.ToListAsync();
        }

        public async Task<Transaction> GetTransactionAsync(Guid id)
        {
            return await _context.Transacations.Include(b => b.Expense).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            return await _context.Transacations.Include(b => b.Expense).ToListAsync();

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
           if(disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

    }
}
