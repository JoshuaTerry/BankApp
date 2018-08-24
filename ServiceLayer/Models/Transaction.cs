using ServiceLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public class Transaction
    {
        public Transaction()
        {
            TransactionDate = DateTime.Now;
        }

        public Transaction(DateTime date)
        {
            TransactionDate = date;
        }
        public Guid Id { get; set; }
        public Guid SourceAccoundId { get; set; }
        public Guid DestinationAccountId { get; set; }
        public DateTime TransactionDate { get; private set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
    }
}
