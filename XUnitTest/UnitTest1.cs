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
            Assert.Equal("Uttaget är större än vad som finns på kontot.", result);

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
            Assert.Equal("Insättningen lyckades. Nytt saldo: 20:-", result);

            //Test that funds have been deposited to the account
            Assert.Equal(_bankRepo._customerWithAccounts.Single(x => x.CustomerNumber == 0).Account.Balance, 20);
            _bankRepo._customerWithAccounts.Remove(newCustomer);
        }

        [Fact]
        public void TransferAmount()
        {
            var bankRepo = BankRepository.Instance();

            int sum = 1;
            var accounts = bankRepo.GetAllCustomerAndAccounts();
            var expectedBalanceFrom = accounts[0].Account.Balance - sum;
            var expecedBalanceTo = accounts[1].Account.Balance + sum;

            bankRepo.Transfer(accounts[0].Account.AccountNumber, accounts[1].Account.AccountNumber, sum);
            Assert.Equal(expectedBalanceFrom, accounts[0].Account.Balance);
            Assert.Equal(expecedBalanceTo, accounts[1].Account.Balance);
        }

        [Fact]
        public void TransferInsufficentAmount()
        {
            var bankRepo = BankRepository.Instance();

            var accounts = bankRepo.GetAllCustomerAndAccounts();
            int sum = accounts[0].Account.Balance + 10;
            var expectedBalanceFrom = accounts[0].Account.Balance;
            var expecedBalanceTo = accounts[1].Account.Balance;

            bankRepo.Transfer(accounts[0].Account.AccountNumber, accounts[1].Account.AccountNumber, sum);
            Assert.Equal(expectedBalanceFrom, accounts[0].Account.Balance);
            Assert.Equal(expecedBalanceTo, accounts[1].Account.Balance);
        }
    }
}
