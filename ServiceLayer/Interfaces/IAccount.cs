using ServiceLayer.Enums;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IAccount
    {
        Guid Id { get; set; } 
        Bank Bank { get; set; }
        Owner Owner { get; set; }
        AccountType AccountType { get; set; }
        decimal Balance { get; set; }
        List<Transaction> Transactions { get; set; }
    }
}
