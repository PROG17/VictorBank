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
            _repository = BankRepository.Instance();
        }

        public IActionResult CustomerAccounts()
        {
            var customerAccounts = _repository.GetAllCustomerAndAccounts();
            return View(customerAccounts);
        }

        public IActionResult DepositWithdraw(string message)
        {
            ViewBag.message = message;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deposit(int accountNumber, int sum)
        {
            if (sum == 0 || accountNumber == 0)
                return RedirectToAction("DepositWithdraw", new { message = "Summan eller kontot är i fel format eller 0" });
            var result = _repository.Deposit(accountNumber, sum);
            return RedirectToAction("DepositWithdraw", new { message = result });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Withdraw(int accountNumber, int sum)
        {
            if (sum == 0 || accountNumber == 0)
                return RedirectToAction("DepositWithdraw", new { message = "Summa eller konto är i fel format eller 0" });
            var result = _repository.Withdraw(accountNumber, sum);
            return RedirectToAction("DepositWithdraw", new { message = result });
        }
    }
}