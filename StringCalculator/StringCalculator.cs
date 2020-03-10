using System;
using System.Collections.Generic;
using System.Linq;
using StringCalculator.FilterRules;

namespace StringCalculator
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            var parser = new StringParser(numbers);
            var result = NumberFilterRules.Get
                .Aggregate(parser.Numbers, (numbers, rule) => rule.Apply(numbers))
                .Sum();
            AddOccured?.Invoke(numbers, result);
            CallCount++;
            return result;
        }

        public int CallCount { get; private set; } = 0;
        public event Action<string, int> AddOccured;
    }
}