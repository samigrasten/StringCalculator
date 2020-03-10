namespace StringCalculator.FilterRules
{
    public interface IFilterRule
    {
        int[] Apply(int[] numbers);
    }
}