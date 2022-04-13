using BankConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsoleApp.Abstraction
{
    public abstract class InvestmentAccount : Account
    {
        public InvestmentAccountType TypeOfInvestmentAccount { get; private set; }

        public InvestmentAccount(long accountNumber, InvestmentAccountType investmentAccountType, Owner owner) : base(accountNumber, AccountType.Investment, owner)
        {
            TypeOfInvestmentAccount = investmentAccountType;
        }

        public InvestmentAccount(long accountNumber, InvestmentAccountType investmentAccountType, decimal addBalance, Owner owner) : base(accountNumber, AccountType.Investment, owner)
        {
            TypeOfInvestmentAccount = investmentAccountType;
            Deposit(addBalance);
        }
    }
}
