// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
namespace BankConsoleApp {
    public class Program {
        public static void Main(string[] args) {
            Console.WriteLine("Hello Nitin");

            var bank = new Bank().SetBankName("Nitin's Bank").AddAccounts(new List<IAccount> { 
            
                new CheckingAccount(1, 500),
                new IndividualInvestmentAcct(2, 2000),
                new CorporateInvestmentAcct(3,10000)
            });


            // deposit
            bank.PerformDepositOrWithdrawal(TransactionType.Deposit, 600, 1);
            Console.WriteLine(bank.CheckBalance(1));


            bank.PerformDepositOrWithdrawal(TransactionType.Deposit, 700, 2);
            Console.WriteLine(bank.CheckBalance(2));

            bank.PerformDepositOrWithdrawal(TransactionType.Deposit, 1700, 3);
            Console.WriteLine(bank.CheckBalance(3));


            //withdraw
            bank.PerformDepositOrWithdrawal(TransactionType.Withdraw, 100, 1);//1000 balance
            Console.WriteLine(bank.CheckBalance(1));

            bank.PerformDepositOrWithdrawal(TransactionType.Withdraw, 150, 2); // 2550 balance
            Console.WriteLine(bank.CheckBalance(2));

            bank.PerformDepositOrWithdrawal(TransactionType.Withdraw, 700, 3); // 11000 balance
            Console.WriteLine(bank.CheckBalance(3));


            //withdraw from Indic


        }
    }
}
