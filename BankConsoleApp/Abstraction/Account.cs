using BankConsoleApp.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsoleApp.Abstraction
{
    public abstract class Account : IAccount
    {
        public long AccountNumber { get; private set; }
        public decimal Balance { get; private set; }
        public AccountType TypeOfAccount { get; private set; }

        public Account(long accountNumber, AccountType accountType)
        {
            if (accountNumber > 0 && accountNumber < 9999999999)
                AccountNumber = accountNumber;
            TypeOfAccount = accountType;
        }

        public virtual decimal Deposit(decimal amount)
        {
            if (amount > 0)
                return Balance += amount;
            throw new InvalidOperationException("Cannot deposit negative amount.");
            //return Balance;
        }

        public virtual decimal Withdraw(decimal amount)
        {
            if (amount > 0 && Balance > amount)
                return Balance -= amount;
            throw new InvalidOperationException("Cannot withdraw amount greater than available balance.");
            // return Balance;
        }
    }
}
