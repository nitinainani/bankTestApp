using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsoleApp.Contract
{
    public interface IAccount
    {
        decimal Deposit(decimal amount);
        decimal Withdraw(decimal amount);

    }
}
