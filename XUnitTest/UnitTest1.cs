using System;
using System.Collections.Generic;
using System.Linq;
using VictorBank.Models;
using Xunit;

namespace XUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void OverDrawAccount()
        {
            var _bankRepo = BankRepository.Instance();

            var newCustomer = new Customer() { CustomerNumber = 0, Name = "Test" };
            var newAccount = new Account() { AccountNumber = 0, Balance = 10 };

            newCustomer.Account = newAccount;

            _bankRepo._customerWithAccounts.Add(newCustomer);

            var result = _bankRepo.Withdraw(0, 100);

            //Test that the result we get back is correct
            Assert.Equal("Uttaget �r st�rre �n vad som finns p� kontot.", result);

            //Test that no funds have been withdrawn from the account
            Assert.Equal(_bankRepo._customerWithAccounts.SingleOrDefault(x => x.CustomerNumber == 0).Account.Balance, 10);

            _bankRepo._customerWithAccounts.Remove(newCustomer);
        }

        [Fact]
        public void WithdrawAmount()
        {
            var _bankRepo = BankRepository.Instance();

            var newCustomer = new Customer() { CustomerNumber = 0, Name = "Test" };
            var newAccount = new Account() { AccountNumber = 0, Balance = 10 };

            newCustomer.Account = newAccount;

            _bankRepo._customerWithAccounts.Add(newCustomer);

            var result = _bankRepo.Withdraw(0, 10);
            //Test that the result we get back is correct
            Assert.Equal("Lyckades ta ut 10:-. Nytt saldo: 0:-", result);

            //Test that funds have been withdrawn from the account
            Assert.Equal(_bankRepo._customerWithAccounts.SingleOrDefault(x => x.CustomerNumber == 0).Account.Balance, 0);
            _bankRepo._customerWithAccounts.Remove(newCustomer);
        }

        [Fact]
        public void DepositAmount()
        {
            var _bankRepo = BankRepository.Instance();

            var newCustomer = new Customer() { CustomerNumber = 0, Name = "Test" };
            var newAccount = new Account() { AccountNumber = 0, Balance = 10 };

            newCustomer.Account = newAccount;

            _bankRepo._customerWithAccounts.Add(newCustomer);

            var result = _bankRepo.Deposit(0, 10);

            //Test that the result we get back is correct
            Assert.Equal("Ins�ttningen lyckades. Nytt saldo: 20:-", result);

            //Test that funds have been deposited to the account
            Assert.Equal(_bankRepo._customerWithAccounts.Single(x => x.CustomerNumber == 0).Account.Balance, 20);
            _bankRepo._customerWithAccounts.Remove(newCustomer);
        }
    }
}
