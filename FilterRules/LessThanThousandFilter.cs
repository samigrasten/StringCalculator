using System.Linq;

namespace StringCalculator.FilterRules
{
    public class LessThanThousandFilter : IFilterRule
    {
        public int[] Apply(int[] numbers)
            => numbers
                .Where(number => number <= 1000)
                .ToArray();
    }
}