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

        public InvestmentAccount(long accountNumber, InvestmentAccountType investmentAccountType) : base(accountNumber, AccountType.Investment)
        {
            TypeOfInvestmentAccount = investmentAccountType;
        }

        public InvestmentAccount(long accountNumber, InvestmentAccountType investmentAccountType, decimal addBalance) : base(accountNumber, AccountType.Investment)
        {
            TypeOfInvestmentAccount = investmentAccountType;
            Deposit(addBalance);
        }
    }
}
