using System;
using System.Linq;

namespace StringCalculator.FilterRules
{
    public class OnlyPosiviteNumbersFilter : IFilterRule
    {
        public int[] Apply(int[] numbers)
        {
            var negativeNumbers = numbers.Where(number => number < 0).ToArray();
            if (!negativeNumbers.Any()) return numbers;

            var negativeNumberList = negativeNumbers
                .Select(n => n.ToString())
                .Aggregate((list, item) => $"{list},{item}");
            throw new ArgumentException($"Negative numbers are not allowed ({negativeNumberList})");
        }
    }
}