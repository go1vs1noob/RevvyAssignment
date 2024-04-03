namespace RevvyTests;
using FluentAssertions;
using Xunit;
public class NumberSumCheckerTests
{
    [Theory]
    [InlineData(10, new int[]{3, 1, 8, 5, 4})]
    public void NumberSumChecker_TargetCanBeSumOfCandidates_ReturnTrue(int target, int[] candidates)
    {
        NumberSumChecker numberSumChecker = new NumberSumChecker(target, candidates);
        bool result = numberSumChecker.TargetCanBeSumOfCandidates();
        result.Should().BeTrue();
    }
}