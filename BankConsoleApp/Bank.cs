using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsoleApp
{
    public class Bank
    {
        public string Name { get; private set; }
        public List<IAccount> Accounts { get; private set; } = new List<IAccount>();



        public Bank SetBankName(string bankName)
        {
            if (!string.IsNullOrWhiteSpace(bankName))
                Name = bankName;
            return this;
        }

        public Bank AddAccount(IAccount account)
        {
            if (account != null)
            {
                Accounts.Add(account);
            }
            return this;
        }

        public Bank AddAccounts(List<IAccount> accounts) { 
          if(accounts != null && accounts.Count > 0)
                Accounts = accounts;
            return this;
        }

        public void PerformDepositOrWithdrawal(TransactionType transactionType, decimal amount, long accountNumber) {

            try
            {
                var account = GetAccount(accountNumber);
                if (account != null)
                {
                    if (transactionType == TransactionType.Withdraw)
                    {
                        account.Withdraw(amount);
                    }
                    else if (transactionType == TransactionType.Deposit)
                    {
                        account.Deposit(amount);
                    }
                    return;
                }
                throw new Exception("Invalid Account Number.");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
                
        }

        public string CheckBalance(long accountNumber) {
            var account = GetAccount(accountNumber);
            if (account != null) {
                var actualAcct = (Account)account;
                var message=  $"Account Balance is {actualAcct.Balance} for Account Number {actualAcct.AccountNumber} Account type {actualAcct.TypeOfAccount}";
                if (actualAcct is IndividualInvestmentAcct)
                    message += $"InvestmentAcctType is IndividualAccount";
                if (actualAcct is CorporateInvestmentAcct)
                    message += $"InvestmentActtType is CorporateAccount";
                return message;
            }
            return "Invalid Account Number";
        }

        public IAccount GetAccount(long accountNumber) {
           return Accounts.FirstOrDefault(x => ((Account)x).AccountNumber == accountNumber);
        }
        
    }
    public interface IAccount
    {
        decimal Deposit(decimal amount);
        decimal Withdraw(decimal amount);

    }
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
            if(amount > 0)
               return Balance += amount;
            throw new InvalidOperationException("Cannot deposit negative amount.");
            //return Balance;
        }

        public virtual decimal Withdraw(decimal amount)
        {
            if(amount > 0 && Balance > amount)
              return  Balance -= amount;
            throw new InvalidOperationException("Cannot withdraw amount greater than available balance.");
           // return Balance;
        }
    }

    public class CheckingAccount : Account {
        public CheckingAccount(long accountNumber) : base(accountNumber, AccountType.Checking)
        { }

        public CheckingAccount(long accountNumber, decimal addBalance) : base(accountNumber, AccountType.Checking)
        { 
          Deposit(addBalance);
        }
    }

    public abstract class InvestmentAccount : Account { 
    
        public InvestmentAccountType TypeOfInvestmentAccount { get; private set;}

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

    public class IndividualInvestmentAcct : InvestmentAccount {
        public IndividualInvestmentAcct(long accountNumber) : base (accountNumber, InvestmentAccountType.Individual)
        {}

        public IndividualInvestmentAcct(long accountNumber, decimal addBalance) : base(accountNumber, InvestmentAccountType.Individual)
        {
            Deposit(addBalance);
        }

        public override decimal Withdraw(decimal amount)
        {
            if(amount <= 500)
            return base.Withdraw(amount);

            throw new Exception("Withdrawal amount for individual investment cannot be more than $500"); 
        }
    }

    public class CorporateInvestmentAcct : InvestmentAccount {
        public CorporateInvestmentAcct(long accountNumber) : base (accountNumber, InvestmentAccountType.Corporate)
        {}
        public CorporateInvestmentAcct(long accountNumber, decimal addBalance) : base(accountNumber, InvestmentAccountType.Corporate)
        {
            Deposit(addBalance);
        }
    }


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
    public enum AccountType { 
     Checking,
     Investment
    }

    public enum InvestmentAccountType { 
      Individual,
      Corporate
    }

    public enum TransactionType { 
      Deposit,
      Withdraw,
      Transfer
    }
}