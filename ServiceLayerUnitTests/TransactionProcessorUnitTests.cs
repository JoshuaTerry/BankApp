using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayerUnitTests
{
    [TestClass]
    public class TransactionProcessorUnitTests
    {
        private Guid sourceAccountId = Guid.Parse("D7ABB652-B6D7-47AF-A945-B25DA5FD6922");
        private Guid destAccountId = Guid.Parse("82CD5D29-34A6-419A-B93D-CCAF5CB910F6");

        [TestMethod]
        public void Deposit_ReturnAccount_Balance300()
        {
            var repo = new Mock<IAccountRepository>();
            repo.Setup(r => r.GetById(sourceAccountId)).Returns(GetSourceAccount());
            repo.Setup(r => r.GetById(sourceAccountId)).Returns(GetDestinationAccount());

            var processor = new TransactionProcessor(repo.Object);
            var account = processor.Deposit(sourceAccountId, 100.00m);

            Assert.AreEqual(300.00m, account.Balance);
        }

        [TestMethod]
        public void Withdraw_ReturnAccount_Balance100()
        {
            var repo = new Mock<IAccountRepository>();
            repo.Setup(r => r.GetById(sourceAccountId)).Returns(GetSourceAccount());
            repo.Setup(r => r.GetById(sourceAccountId)).Returns(GetDestinationAccount());

            var processor = new TransactionProcessor(repo.Object);
            var account = processor.Withdraw(sourceAccountId, 100.00m);

            Assert.AreEqual(100.00m, account.Balance);
        }

        [TestMethod]
        public void Withdraw_ReturnAccount_WithdrawAmountExceeded()
        {

        }
        private Account GetSourceAccount()
        {
            var account = new Account();
            account.Id = sourceAccountId;
            account.Owner = new Owner() { FirstName = "Test", LastName = "User" };
            account.Transactions = new List<Transaction>();
            account.Balance = 200.00m;
            return account;
        }

        private Account GetDestinationAccount()
        {
            var account = new Account();
            account.Id = destAccountId;
            account.Owner = new Owner() { FirstName = "Test2", LastName = "User2" };
            account.Transactions = new List<Transaction>();
            account.Balance = 200.00m;
            return account;
        }
    }
}
