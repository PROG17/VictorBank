using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VictorBank.Models
{
    public class BankRepository
    {
        public List<Customer> GetAllCustomerAndAccounts()
        {
            return CustomerAndAccount();
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



    }
}
