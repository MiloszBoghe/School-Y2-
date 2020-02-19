using System;
using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Addition_1and2_Result3()
        {
            //Arrange
            int a = 1;
            int b = 2;
            int result;
            int expectedResult = 3;
            //Act
            result = Calculator.Calculator.Addition(1, 2);

            //Assert
            Assert.AreEqual(result , expectedResult);

        }

        [TestMethod]
        public void Faculty_5_Result120()
        {
            int a = 5;
            long result;
            long expectedResult = 120;

            result = Calculator.Calculator.Faculty(5);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
