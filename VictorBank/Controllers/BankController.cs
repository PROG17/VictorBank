using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VictorBank.Models;

namespace VictorBank.Controllers
{
    public class BankController : Controller
    {
        public BankRepository _repository;

        public BankController()
        {
            _repository = new BankRepository();
        }

        public IActionResult CustomerAccounts()
        {
            var customerAccounts = _repository.GetAllCustomerAndAccounts();
            return View(customerAccounts);
        }
    }
}