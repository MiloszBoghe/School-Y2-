using Bank.Business;
using Bank.DomainClasses;
using Bank.DomainClasses.Enums;
using NUnit.Framework;

namespace Bank.Test
{
    [TestFixture]
    public class AccountManagerTests
    {
        private AccountManager accountManager;
        private Account fromYouthAccount;
        private Account fromOtherAccount;
        private Account toAccount;

        [SetUp]
        public void SetUp()
        {
            accountManager = new AccountManager();

            toAccount = new Account()
            {
                Balance = 500
            };
        }

        [Test]
        public void ShouldCorrectlyTransferMoneyWhenBalanceIsSufficient()
        {
            CreateOtherAccount();
            accountManager.TransferMoney(fromOtherAccount, toAccount, 1000);
            Assert.That(fromOtherAccount.Balance, Is.EqualTo(1000));
            Assert.That(toAccount.Balance, Is.EqualTo(1500));
        }

        [Test]
        public void ShouldThrowInvalidTransferExceptionWhenBalanceIsInsufficient()
        {
            CreateOtherAccount();
            Assert.Throws<InvalidTransferException>(() => accountManager.TransferMoney(fromOtherAccount, toAccount, 5000));
        }

        [Test]
        public void ShouldThrowInvalidTransferExceptionForYouthAccountWhenAmountIsBiggerThan1000()
        {
            CreateYouthAccount();
            Assert.Throws<InvalidTransferException>(() => accountManager.TransferMoney(fromYouthAccount, toAccount, 2000));
        }

        public void CreateYouthAccount()
        {
            fromYouthAccount = new Account()
            {
                Balance = 3000,
                AccountType = AccountType.YouthAccount
            };
        }

        public void CreateOtherAccount()
        {
            fromOtherAccount = new Account()
            {
                Balance = 2000
            };
        }
    }
}
