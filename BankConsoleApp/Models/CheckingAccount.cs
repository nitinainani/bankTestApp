using BankConsoleApp.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsoleApp.Models
{
    public class CheckingAccount : Account
    {
        public CheckingAccount(long accountNumber, Owner owner) : base(accountNumber, AccountType.Checking, owner)
        { }

        public CheckingAccount(long accountNumber, decimal addBalance, Owner owner) : base(accountNumber, AccountType.Checking, owner)
        {
            Deposit(addBalance);
        }
    }
}
