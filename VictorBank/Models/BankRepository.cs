using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VictorBank.Models
{
    public class BankRepository
    {
        public List<Customer> _customerWithAccounts;
        private static BankRepository _instance;

        public List<Customer> GetAllCustomerAndAccounts()
        {
            return _customerWithAccounts;
        }

        public static BankRepository Instance()
        {
            if (_instance == null)
                _instance = new BankRepository();
            return _instance;
        }

        private BankRepository()
        {
            _customerWithAccounts = CustomerAndAccount();
        }

        private List<Customer> CustomerAndAccount()
        {
            var customer1 = new Customer() {CustomerNumber = 1, Name = "person1"};
            var customer2 = new Customer(){CustomerNumber = 2, Name = "person2"};
            var customer3 = new Customer(){CustomerNumber = 3, Name = "person3"};

            var account1 = new Account(){AccountNumber = 1, Balance = 10};
            var account2 = new Account(){AccountNumber = 2, Balance = 20};
            var account3 = new Account(){AccountNumber = 3, Balance = 30};

            customer1.Account = account1;
            customer2.Account = account2;
            customer3.Account = account3;

            return new List<Customer>(){customer1, customer2, customer3};
        }

        private Account GetAccount(int accountNumber)
        {
            return _customerWithAccounts.Select(x => x.Account)
                .SingleOrDefault(y => y.AccountNumber == accountNumber);
        }

        public string Withdraw(int withdrawAccountNumber, int amount)
        {
            var withdrawAccount = GetAccount(withdrawAccountNumber);
            if (withdrawAccount == null)
                return "Kontot finns inte.";
            if (withdrawAccount.Balance < amount)
                return "Uttaget är större än vad som finns på kontot.";
            withdrawAccount.Balance -= amount;
            return $"Lyckades ta ut {amount}:-. Nytt saldo: {withdrawAccount.Balance}:-";
        }

        public string Deposit(int depositAccountNumber, int amount)
        {
            var depositAccount = GetAccount(depositAccountNumber);
            if (depositAccount == null)
                return "Kontot finns inte";
            if (amount < 0)
                return "Kan ej sätta in en negativ summa";
            depositAccount.Balance += amount;
            return $"Insättningen lyckades. Nytt saldo: {depositAccount.Balance}:-";
        }

        public string Transfer(int fromAccountNumber, int toAccountNumber, int amount)
        {
            string message = "";

            var fromAccount = GetAccount(fromAccountNumber);
            var toAccount = GetAccount(toAccountNumber);
            if (fromAccount == null)
            {
                message = $"Konto {fromAccountNumber} finns inte";
            }
            else if (toAccount == null)
            {
                message = $"Konto {toAccountNumber} finns inte";
            }
            else if (fromAccountNumber == toAccountNumber)
            {
                message = "Två olika konton måste anges";
            }
            else if (amount < 0)
            {
                message = "Belopp kan inte vara negativt";
            }
            else if (amount > fromAccount.Balance)
            {
                message = $"Inte tillräckligt med pengar på det överförande kontot";
            }
            else
            {
                fromAccount.Balance -= amount;
                toAccount.Balance += amount;
                message =$"Överföring lyckades. Saldo konto {fromAccountNumber}: {fromAccount.Balance}:-. Saldo konto {toAccountNumber}: {toAccount.Balance}:-.";
            }

            return message;
        }

    }
}
