using NumberConverter.UI.Converters;
using NUnit.Framework;
using System;

namespace NumberConverter.UI.Tests
{
    [TestFixture]
    public class RomanNumberConverterTests
    {
        private RomanNumberConverter conv;
        private object value;
        private object result;

        [SetUp]
        public void SetUp()
        {
            conv = new RomanNumberConverter();
        }

        [Test]
        public void Convert_ShouldThrowArgumentExceptionWhenValueIsNotAString()
        {
            //Arrange
            value = new object();

            //Act & Assert            
            Assert.That(() => conv.Convert(value, null, null, null), Throws.ArgumentException.With.Message.Contains("string"));
        }

        [TestCase("")]
        [TestCase("dza564daz")]
        [TestCase("aaaaa")]
        public void Convert_ShouldReturnInvalidNumberWhenTheValueCannotBeParsedAsAnInteger(string num)
        {   //Arrange
            bool isInt;
            int result;
            var res = new Object();

            //Act
            isInt = int.TryParse(num, out result);
            res = conv.Convert(num, null, null, null);

            //Assert
            Assert.That(!isInt);
            Assert.That(res, Is.EqualTo("Invalid number"));

        }

        [TestCase("100346")]
        [TestCase("-10346")]
        public void Convert_ShouldReturnOutOfRangeWhenTheValueIsNotBetweeOneAnd3999(string num)
        {
            int number = Convert.ToInt32(num);
            Assert.That(number <= 0 || number >= 3999);
            Assert.That(conv.Convert(num, null, null, null), Is.EqualTo("Out of Roman range (1-3999)"));
        }

        [TestCase("4", "IV")]
        [TestCase("5", "V")]
        [TestCase("50", "L")]
        [TestCase("1000", "M")]
        public void Convert_ShouldCorrectlyConvertValidNumbers(string num1, string num2)
        {
            //Arrange
            int number = Convert.ToInt32(num1);
            bool isNumber;
            int res;
            isNumber = int.TryParse(num2, out res);

            //Act
            string result = conv.ConvertToRoman(number);

            //Assert
            Assert.That(isNumber, Is.False);
            Assert.That(result, Is.EqualTo(num2));
            Assert.That(conv.Convert(num1, null, null, null), Is.EqualTo(conv.ConvertToRoman(number)));

        }

    }
}
