using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace TrackYourExpenseApi.Models
{
    public class ExpenseDto
    {

        public Guid Id { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public string TransactionType { get; set; }

    }
}
