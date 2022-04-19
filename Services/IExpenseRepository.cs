using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackYourExpenseApi.Entities;

namespace TrackYourExpenseApi.Services
{
    public interface IExpenseRepository
    {
        //IEnumerable<Entities.Expense> GetExpenses();
        //Entities.Expense GetExpense(int id);    

        public Task<IEnumerable<Expense>> GetExpensesAsync();
        public Task<Expense> GetExpenseAsync(Guid id);

        public Task<IEnumerable<Transaction>> GetTransactionsAsync();
        public Task<Transaction> GetTransactionAsync(Guid id);


    }
}
