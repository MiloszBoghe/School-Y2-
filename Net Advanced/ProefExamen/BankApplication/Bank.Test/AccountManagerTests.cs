using Bank.Business;
using Bank.DomainClasses;
using Bank.DomainClasses.Enums;
using NUnit.Framework;

namespace Bank.Test
{
    [TestFixture]
    public class AccountManagerTests
    {
        [Test]
        public void ShouldCorrectlyTransferMoneyWhenBalanceIsSufficient()
        {
            // TODO: vul de test aan
        }

        [Test]
        public void ShouldThrowInvalidTransferExceptionWhenBalanceIsInsufficient()
        {
            // TODO: vul de test aan
        }

        [Test]
        public void ShouldThrowInvalidTransferExceptionForYouthAccountWhenAmountIsBiggerThan1000()
        {
            // TODO: vul de test aan
        }
    }
}
