using NUnit.Framework;

namespace CalculatorKata.Tests
{
    public class Tests
    {
        private Calculator _calculator;
        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [Test]
        public void Add_1And5_Results6()
        {
            int result;

            result = _calculator.Add("1,5");

            Assert.That(result.Equals(6));
        }

        [Test]
        public void Add_ContainsInvalidCharacter_ThrowsException()
        {
               
        }

        [Test]
        public void Add_AnyAmountOfNumbers_Succeeds()
        {
               
        }

        [Test]
        public void Add_NegativeNumber_ThrowsException()
        {
               
        }

    }
}