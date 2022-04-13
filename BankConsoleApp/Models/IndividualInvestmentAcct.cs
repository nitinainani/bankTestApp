using BankConsoleApp.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsoleApp.Models
{
    public class IndividualInvestmentAcct : InvestmentAccount
    {
        public IndividualInvestmentAcct(long accountNumber, Owner owner) : base(accountNumber, InvestmentAccountType.Individual, owner)
        { }

        public IndividualInvestmentAcct(long accountNumber, decimal addBalance, Owner owner) : base(accountNumber, InvestmentAccountType.Individual, owner)
        {
            Deposit(addBalance);
        }

        public override decimal Withdraw(decimal amount)
        {
            if (amount <= 500)
                return base.Withdraw(amount);

            throw new Exception("Withdrawal amount for individual investment cannot be more than $500");
        }
    }
}
