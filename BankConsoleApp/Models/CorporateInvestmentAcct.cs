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
        public CorporateInvestmentAcct(long accountNumber, Owner owner) : base(accountNumber, InvestmentAccountType.Corporate, owner)
        { }
        public CorporateInvestmentAcct(long accountNumber, decimal addBalance, Owner owner) : base(accountNumber, InvestmentAccountType.Corporate, owner)
        {
            Deposit(addBalance);
        }
    }
}
