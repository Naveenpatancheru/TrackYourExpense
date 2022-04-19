using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackYourExpenseApi.Entities
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Text { get; set; }

        [Required]
        public double Amount { get; set; }

        public string Description { get; set; }

        public DateTime DateOfTransaction { get; set; }

        [ForeignKey("ExpenseId")]
        public Expense Expense { get; set; }

    }
}

