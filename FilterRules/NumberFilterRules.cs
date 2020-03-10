using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator.FilterRules
{
    public class NumberFilterRules
    {
        public static List<IFilterRule> Get => FilterRules;

        private static readonly List<IFilterRule> FilterRules = new List<IFilterRule>
        {
            new OnlyPosiviteNumbersFilter(),
            new LessThanThousandFilter()
        };

    }
}
