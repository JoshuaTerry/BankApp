using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface ITransactionProcessor
    {
        IAccount Deposit(Guid accountId, decimal amount);
        IAccount Withdraw(Guid accountId, decimal amount);
        List<IAccount> Transfer(Guid sourceAccountId, Guid destinationAccountId, decimal amount);
    }
}
