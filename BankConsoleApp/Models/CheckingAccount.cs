﻿using BankConsoleApp.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsoleApp.Models
{
    public class CheckingAccount : Account
    {
        public CheckingAccount(long accountNumber) : base(accountNumber, AccountType.Checking)
        { }

        public CheckingAccount(long accountNumber, decimal addBalance) : base(accountNumber, AccountType.Checking)
        {
            Deposit(addBalance);
        }
    }
}
