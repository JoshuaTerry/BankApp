using ServiceLayer.Enums;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    //Alternatively the IAccount could be a generic Account definition and 
    // each type of account a new implementation
    public class Account : IAccount
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Bank Bank { get; set; }
        public Owner Owner { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
