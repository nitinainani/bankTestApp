using BankConsoleApp.Abstraction;
using BankConsoleApp.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankConsoleApp.Models
{
    public class Bank
    {
        public string Name { get; private set; }
        public List<IAccount> Accounts { get; private set; } = new List<IAccount>();



        public Bank SetBankName(string bankName)
        {
            if (!string.IsNullOrWhiteSpace(bankName))
                Name = bankName;
            else
                throw new Exception("BankName is Required");
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

        public Bank AddAccounts(List<IAccount> accounts)
        {
            if (accounts != null && accounts.Count > 0)
                Accounts = accounts;
            return this;
        }

        public void PerformTransfer(long fromAccountNumber, long toAccountNumber, decimal amount)
        {
            try
            {
                var fromAccount = GetAccount(fromAccountNumber);
                var toAccount = GetAccount(toAccountNumber);

                if (fromAccount != null && toAccount != null)
                {
                    fromAccount.Withdraw(amount);
                    toAccount.Deposit(amount);
                }
                else
                {
                    throw new Exception("Please make sure both account numbers are valid account");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }

        
        public void PerformDepositOrWithdrawal(TransactionType transactionType, decimal amount, long accountNumber)
        {
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


        //this can be a service that can be injected
        public string CheckBalance(long accountNumber)
        {
            var account = GetAccount(accountNumber);
            if (account != null)
            {
                var actualAcct = (Account)account;
                var message = $"Account Balance is {actualAcct.Balance} for Account Number {actualAcct.AccountNumber} Account type {actualAcct.TypeOfAccount}";
                if (actualAcct is IndividualInvestmentAcct)
                    message += $"InvestmentAcctType is IndividualAccount";
                if (actualAcct is CorporateInvestmentAcct)
                    message += $"InvestmentActtType is CorporateAccount";
                return message;
            }
            return "Invalid Account Number";
        }

        // this can be a service and can be injected
        public IAccount GetAccount(long accountNumber)
        {
            return Accounts.FirstOrDefault(x => ((Account)x).AccountNumber == accountNumber);
        }

    }
}
