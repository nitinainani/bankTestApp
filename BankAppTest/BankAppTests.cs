using BankConsoleApp;
using BankConsoleApp.Abstraction;
using BankConsoleApp.Contract;
using BankConsoleApp.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAppTest
{
    
    [Parallelizable(ParallelScope.Self)]
    public class BankAppTests
    {
        private readonly Bank bank;
        private Account firstAccount;
        private Account secondAccount;
        private Account thirdAccount;

        // When there are objects injected we can use mock framework to mock objects
        public BankAppTests()
        {
            bank = new Bank().SetBankName("Nitin's Bank").AddAccounts(new List<IAccount> {
                new CheckingAccount(1, 500, new Owner(99, "Test")),
                new IndividualInvestmentAcct(2, 2000, new Owner(99, "Test")),
                new CorporateInvestmentAcct(3, 10000, new Owner(99, "Test"))
            });
        }
        [SetUp]
        public void Setup()
        {
             firstAccount = (Account)bank.GetAccount(1);
             secondAccount = (Account)bank.GetAccount(2);
             thirdAccount = (Account)bank.GetAccount(3);
        }
        [Test]
        public void TestAccountCreation()
        {
            Assert.NotNull(bank.Accounts);
            Assert.True(bank.Accounts.Count > 0);
            Assert.Positive(((Account)bank.Accounts.FirstOrDefault()).AccountNumber);
            Assert.AreEqual("Account Balance is 500 for Account Number 1 Account type Checking", bank.CheckBalance(1));
            Assert.AreEqual("Account Balance is 2000 for Account Number 2 Account type InvestmentInvestmentAcctType is IndividualAccount", bank.CheckBalance(2));
            Assert.AreEqual("Account Balance is 10000 for Account Number 3 Account type InvestmentInvestmentActtType is CorporateAccount", bank.CheckBalance(3));
        }

        [Test]
        public void TestAccountValidDeposit()
        {
            bank.PerformDepositOrWithdrawal(TransactionType.Deposit, 600, 1); 
            //var firstAccount = (Account)bank.GetAccount(1);

            bank.PerformDepositOrWithdrawal(TransactionType.Deposit, 700, 2);
            //var secondAccount = (Account)bank.GetAccount(2);

            bank.PerformDepositOrWithdrawal(TransactionType.Deposit, 1700, 3); 
            //var thirdAccount = (Account)bank.GetAccount(3);

            Assert.AreEqual(1100, firstAccount.Balance);
            Assert.AreEqual(2700, secondAccount.Balance);
            Assert.AreEqual(11700, thirdAccount.Balance);
        }

        [Test]
        public void TestAccountInValidDeposit_DoesNotThrowError_BalanceRemainsSame()
        {

            //var firstAccount = (Account)bank.GetAccount(1);
            //var secondAccount = (Account)bank.GetAccount(2);
            //var thirdAccount = (Account)bank.GetAccount(3);

            
            Assert.DoesNotThrow(() => bank.PerformDepositOrWithdrawal(TransactionType.Deposit, -1, 1));
            Assert.DoesNotThrow(() => bank.PerformDepositOrWithdrawal(TransactionType.Deposit, -1, 2));
            Assert.DoesNotThrow(() => bank.PerformDepositOrWithdrawal(TransactionType.Deposit, 0, 3));
            Assert.AreEqual(500, firstAccount.Balance);
            Assert.AreEqual(2000, secondAccount.Balance);
            Assert.AreEqual(10000, thirdAccount.Balance);
        }

        [Test]
        public void TestAccountValidWithdrawal()
        {
            bank.PerformDepositOrWithdrawal(TransactionType.Withdraw, 100, 1);
            bank.PerformDepositOrWithdrawal(TransactionType.Withdraw, 150, 2); 
            bank.PerformDepositOrWithdrawal(TransactionType.Withdraw, 700, 3); 

            //var firstAccount = (Account)bank.GetAccount(1);

            //var secondAccount = (Account)bank.GetAccount(2);

            //var thirdAccount = (Account)bank.GetAccount(3);

            Assert.AreEqual(1000, firstAccount.Balance);
            Assert.AreEqual(2550, secondAccount.Balance);
            Assert.AreEqual(11000, thirdAccount.Balance);
        }

        [Test]
        public void TestAccountInValidWithdrawal_DoesNotThrowError_BalanceRemainsSame()
        {

            //var firstAccount = (Account)bank.GetAccount(1);
            //var secondAccount = (Account)bank.GetAccount(2);
            //var thirdAccount = (Account)bank.GetAccount(3);


            Assert.DoesNotThrow(() => bank.PerformDepositOrWithdrawal(TransactionType.Withdraw, -1, 1));
            Assert.DoesNotThrow(() => bank.PerformDepositOrWithdrawal(TransactionType.Withdraw, -1, 2));
            Assert.DoesNotThrow(() => bank.PerformDepositOrWithdrawal(TransactionType.Withdraw, 0, 3));
            Assert.AreEqual(500, firstAccount.Balance);
            Assert.AreEqual(2000, secondAccount.Balance);
            Assert.AreEqual(10000, thirdAccount.Balance);
        }

        [Test]
        public void TestBankNameNotProvided_ThrowsException()
        {
            Assert.Throws<Exception>(() => new Bank().SetBankName(""));
        }
        [Test]
        public void TestInvestmentPersonalAccount_MoreThan500Withdrawal_DoesNotWithDraw_BalanceRemainsSame()
        {
            var bankIndInv = new Bank().SetBankName("Nitin's Bank").AddAccounts(new List<IAccount> {
                new CheckingAccount(1, 500, new Owner(99, "Test")),
                new IndividualInvestmentAcct(2, 2000, new Owner(99, "Test")),
                new CorporateInvestmentAcct(3, 10000, new Owner(99, "Test"))
            });
            bankIndInv.PerformDepositOrWithdrawal(TransactionType.Withdraw, 501, 2);
            var secondAccountInvest = (Account)bankIndInv.GetAccount(2);
            Assert.AreEqual(2000, secondAccountInvest.Balance);
        }
        [Test]
        public void TestValidTransfer() {
            var firstAcctBeforeTransfer = firstAccount.Balance;
            var secondAcctBeforeTransfer = secondAccount.Balance;
            bank.PerformTransfer(1, 2, 100);
            Assert.True( firstAccount.Balance == firstAcctBeforeTransfer - 100);
            Assert.True(secondAccount.Balance == secondAcctBeforeTransfer + 100); 
        }

        [Test]
        public void TestOverDraftTransfer_ItWillShowBalanceUnchanged() {
            var firstAcctBeforeTransfer = firstAccount.Balance;
            var secondAcctBeforeTransfer = secondAccount.Balance;
            bank.PerformTransfer(1, 2, 10000);
            Assert.True(firstAccount.Balance == firstAcctBeforeTransfer );
            Assert.True(secondAccount.Balance == secondAcctBeforeTransfer);
        }

        [Test]
        public void TestInvalidAccountNumber_AccountDoesNotExist_ErrorHandled()
        {
            Assert.DoesNotThrow(() => bank.PerformTransfer(5, 6, 100));
        }

        [Test]
        public void TestAddInvalidOnwerValue_ThrowsException() {
            Assert.Throws<Exception>(() => new CheckingAccount(123, new Owner(-1, "")));
            Assert.Throws<Exception>(() => new CheckingAccount(123, null));
        }
    }
}
