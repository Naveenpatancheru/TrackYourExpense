using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrackYourExpenseApi.Entities
{
    public class Expense
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }   
        
        public string Description { get; set; }

        public Transaction Transaction { get; set; }
        //public ICollection<Transaction> Transactions { get; set; }
        //     = new List<Transaction>();
    }
}
