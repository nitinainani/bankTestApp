using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsoleApp
{

    //Below enums can be replaced with Static class as well and we can discuss pros and cons of enum
    //public static class AccountType { 
    //    public static string AccountCode { get; private set; }
    //    public static string AccountDescription { get; private set; }

    //    public static AccountType(string code, string description)
    //    {
    //        AccountCode = code;
    //        AccountDescription = description;
    //    }

    //    public static AccountType Checking = new AccountType("Checking", "This is a checking account");


    //}
    public enum AccountType
    {
        Checking,
        Investment
    }

    public enum InvestmentAccountType
    {
        Individual,
        Corporate
    }

    public enum TransactionType
    {
        Deposit,
        Withdraw,
        Transfer
    }
}
