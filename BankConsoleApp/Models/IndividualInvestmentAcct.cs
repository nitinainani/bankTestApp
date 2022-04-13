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
        public IndividualInvestmentAcct(long accountNumber) : base(accountNumber, InvestmentAccountType.Individual)
        { }

        public IndividualInvestmentAcct(long accountNumber, decimal addBalance) : base(accountNumber, InvestmentAccountType.Individual)
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
