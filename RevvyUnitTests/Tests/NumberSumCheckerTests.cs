using Revvy;
using FluentAssertions;

namespace RevvyUnitTests;

public class NumberSumCheckerTests
{
    [Theory]
    [InlineData(10, new int[] { 3, 1, 8, 5, 4 })]
    [InlineData(7, new int[] { 2, 3, 2 })]
    [InlineData(100, new int[] { 1, 2, 3, 4, 5, 85 })]
    [InlineData(500, new int[] { 100, 100, 100, 100, 100, 1, 1, 1, 1, 1, 1 })]
    [InlineData(0, new int[] { 1, 2, 3 })]
    public void NumberSumChecker_TargetCanBeSumOfCandidates_ReturnTrue(int target, int[] candidates)
    {
        var numberSumChecker = new NumberSumChecker(target, candidates);
        var result = numberSumChecker.TargetCanBeSumOfCandidates();
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(2, new int[] { 3, 1, 8, 5, 4 })]
    [InlineData(10, new int[] { })]
    [InlineData(100, new int[] { 1, 2, 3, 3, 5, 85 })]
    [InlineData(5, new int[] { 10, 20, 30 })]
    public void NumberSumChecker_TargetCanBeSumOfCandidates_ReturnFalse(int target, int[] candidates)
    {
        var numberSumChecker = new NumberSumChecker(target, candidates);
        var result = numberSumChecker.TargetCanBeSumOfCandidates();
        result.Should().BeFalse();
    }
}