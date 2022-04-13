// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using BankConsoleApp.Contract;
using BankConsoleApp.Models;

namespace BankConsoleApp {
    public class Program {
        public static void Main(string[] args) {
            Console.WriteLine("Hello Nitin");
            // As this is data part it should be part of Repo
            var bank = new Bank().SetBankName("Nitin's Bank").AddAccounts(new List<IAccount> { 
            
                new CheckingAccount(1, 500),
                new IndividualInvestmentAcct(2, 2000),
                new CorporateInvestmentAcct(3,10000)
            });


            // deposit
            bank.PerformDepositOrWithdrawal(TransactionType.Deposit, 600, 1); //1100 Balance
            Console.WriteLine(bank.CheckBalance(1));


            bank.PerformDepositOrWithdrawal(TransactionType.Deposit, 700, 2); // 2700 Balance
            Console.WriteLine(bank.CheckBalance(2));

            bank.PerformDepositOrWithdrawal(TransactionType.Deposit, 1700, 3); // 11700 Balance
            Console.WriteLine(bank.CheckBalance(3));


            //withdraw
            bank.PerformDepositOrWithdrawal(TransactionType.Withdraw, 100, 1);//1000 balance
            Console.WriteLine(bank.CheckBalance(1));

            bank.PerformDepositOrWithdrawal(TransactionType.Withdraw, 150, 2); // 2550 balance
            Console.WriteLine(bank.CheckBalance(2));

            bank.PerformDepositOrWithdrawal(TransactionType.Withdraw, 700, 3); // 11000 balance
            Console.WriteLine(bank.CheckBalance(3));


            //withdraw from Individual Account more than 500

            bank.PerformDepositOrWithdrawal(TransactionType.Withdraw, 501, 2); // Invalid operation


            // perform transfer from checking to Individual InvestmentAccount

            bank.PerformTransfer(1, 2, 100);
            Console.WriteLine(bank.CheckBalance(1)); // 900 Balance
            Console.WriteLine(bank.CheckBalance(2)); // 2650 Balance


            //perform overdraft transfer and get error message

            bank.PerformTransfer(1, 2, 1000);

            // invalid account Number error Message
            bank.PerformTransfer(5, 6, 100);

        }
    }
}
