using BankConsoleApp.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsoleApp.Models
{
    public class CorporateInvestmentAcct : InvestmentAccount
    {
        public CorporateInvestmentAcct(long accountNumber) : base(accountNumber, InvestmentAccountType.Corporate)
        { }
        public CorporateInvestmentAcct(long accountNumber, decimal addBalance) : base(accountNumber, InvestmentAccountType.Corporate)
        {
            Deposit(addBalance);
        }
    }
}
