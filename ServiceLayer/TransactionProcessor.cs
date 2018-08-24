using ServiceLayer.Enums;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class TransactionProcessor : ITransactionProcessor
    {
        private IAccountRepository repository = null;
        // For unit testing and possible DI
        public TransactionProcessor(IAccountRepository repository)
        {
            this.repository = repository;
        }
        public IAccount Deposit(Guid accountId, decimal amount)
        {
            var account = repository.GetById(accountId);
            if (account == null)
                throw new Exception("No Account was found with this Id.");

            if (amount <= 0)
                throw new Exception("Must Deposit a positive amount.");

            ProcessDepositWithdraw(account, amount, TransactionType.Deposit);

            return account;
        }

        public List<IAccount> Transfer(Guid sourceAccountId, Guid destinationAccountId, decimal amount)
        {
            var sourceAccount = repository.GetById(sourceAccountId);
            var destAccount = repository.GetById(sourceAccountId);
            if (sourceAccount == null || destAccount == null)
                throw new Exception("No Account was found with this Id.");

            if (amount <= 0)
                throw new Exception("Must transfer a positive amount.");

            var accounts = new List<Account>();
            accounts.Add(sourceAccount);
            accounts.Add(destAccount);
            ProcessDepositWithdraw(sourceAccount, amount, TransactionType.Withdraw);
            ProcessDepositWithdraw(destAccount, amount, TransactionType.Deposit);

            return accounts.Cast<IAccount>().ToList();
        }

        public IAccount Withdraw(Guid accountId, decimal amount)
        {
            var account = repository.GetById(accountId);
            if (account == null)
                throw new Exception("No Account was found with this Id.");

            if (amount <= 0)
                throw new Exception("Must Deposit a positive amount.");

            ProcessDepositWithdraw(account, -amount, TransactionType.Withdraw);

            return account;
        }

        //Account is a reference type and so doesn't have to be returned
        private void ProcessDepositWithdraw(Account account, decimal amount, TransactionType tranType)
        {
            if (tranType == TransactionType.Withdraw && account.AccountType == AccountType.Individual)
            {
                var totalWithdrawsToday = account.Transactions.Where(t => t.TransactionType == TransactionType.Withdraw && t.TransactionDate.Date == DateTime.Today.Date).Sum(t => t.Amount);
                var tempTotal = totalWithdrawsToday - Math.Abs(amount);
                if (tempTotal > 1000)
                    throw new Exception("Withdraws on individual accounts are limited to $1000 per day.");
            }

            var tran = new Transaction();
            tran.Amount = amount;
            tran.TransactionType = tranType;
            tran.SourceAccoundId = account.Id;
            account.Transactions.Add(tran);
            account.Balance += amount;            
        }

        private void ProcessTransfer(Account source, Account destination, decimal amount)
        {

        }
    }
}
