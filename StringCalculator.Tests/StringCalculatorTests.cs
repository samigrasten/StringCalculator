using System;
using NUnit.Framework;

namespace StringCalculator.Tests
{
    public class StringCalculatorTests
    {
        [Test]
        [TestCase("", 0)]
        [TestCase("1", 1)]
        [TestCase("1,2", 3)]
        [TestCase("1\n2,3", 6)]
        public void NumbersInCommaSeparatedStringAreSummedUp(string numbers, int expectedResult)
        {
            var sut = CreateSut();
            var result = sut.Add(numbers);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("//;\n1;2;4", 7)]
        public void DefaultDelimitersCanBeChanged(string numbers, int expectedResult)
        {
            var sut = CreateSut();
            var result = sut.Add(numbers);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        
        [Test]
        [TestCase("1,-2,4", "Negative numbers are not allowed (-2)")]
        [TestCase("1,-2,-4", "Negative numbers are not allowed (-2,-4)")]
        public void NegativeNumbersAreNotAllowed(string numbers, string expectedMessage)
        {
            var sut = CreateSut();
            var exception = Assert.Throws<ArgumentException>(() => sut.Add(numbers));
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void CallingTimesForAddMethodCanBeFetched()
        {
            var sut = CreateSut();
            var numbers = "";
            sut.Add(numbers);
            Assert.That(sut.CallCount, Is.EqualTo(1));
        }
        
        [Test]
        [TestCase("1,1000,1001", 1001)]
        public void NumberWhichAreBiggerThanThousandWillBeIgnored(string numbers, int expectedResult)
        {
            var sut = CreateSut();
            var result = sut.Add(numbers);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("//;\n1;2;4", 7)]
        [TestCase("//[xxx]\n1xxx2xxx4", 7)]
        public void DelimitersCanBeOfAnyLength(string numbers, int expectedResult)
        {
            var sut = CreateSut();
            var result = sut.Add(numbers);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("//[;][-]\n1;2-4", 7)]
        [TestCase("//[xxx][***]\n1xxx2***4", 7)]
        public void MultipleDelimitersCanBeDefined(string numbers, int expectedResult)
        {
            var sut = CreateSut();
            var result = sut.Add(numbers);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
        
        [Test]
        public void EventOccursOnEveryAddCall()
        {
            var sut = CreateSut();
            (string numbers, int sum) result = (string.Empty, 0);
            sut.AddOccured += (numbers, sum) =>
            {
                result = (numbers, sum);
            };

            sut.Add("1,3");
            Assert.That(result.numbers, Is.EqualTo("1,3"));
            Assert.That(result.sum, Is.EqualTo(4));
        }

        private static StringCalculator CreateSut()
        {
            return new StringCalculator();
        }
    }
}