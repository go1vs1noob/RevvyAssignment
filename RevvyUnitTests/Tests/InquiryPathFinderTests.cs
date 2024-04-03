using Revvy;
using FluentAssertions;

namespace RevvyUnitTests.Tests;

public class InquiryPathFinderTests
{
    [Theory]
    [MemberData(nameof(GoodInquiryPathFinderTestData))]
    public void InquiryPathFinder_FindPath_ReturnListInt(HashSet<IOfficial> officialsDependencySet, List<int> expected)
    {
        var pathFinder = new InquiryPathFinder(officialsDependencySet);
        pathFinder.FindPath();
        pathFinder.ResultPath.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    [Theory]
    [MemberData(nameof(CyclicInquiryPathFinderTestData))]
    public void InquiryPathFinder_FindPath_ThrowException(HashSet<IOfficial> officialsDependencySet)
    {
        var pathFinder = new InquiryPathFinder(officialsDependencySet);
        var act = () => pathFinder.FindPath();
        act.Should().Throw<Exception>().WithMessage("Cycle found");
    }

    public static IEnumerable<object[]> GoodInquiryPathFinderTestData()
    {
        yield return new object[]
        {
            new HashSet<IOfficial>
            {
                new Official()
                {
                    Id = 1, RequiresInquiryFrom = new HashSet<int>() { 2 }
                },
                new Official()
                {
                    Id = 2, RequiresInquiryFrom = new HashSet<int>() { 3, 4 }
                }
            },
            new List<int>() { 4, 3, 2, 1 }
        };
        yield return new object[]
        {
            new HashSet<IOfficial>
            {
                new Official()
                {
                    Id = 1, RequiresInquiryFrom = new HashSet<int>() { 2 }
                },
                new Official()
                {
                    Id = 3, RequiresInquiryFrom = new HashSet<int>() { 4 }
                }
            },
            new List<int>() { 2, 1, 4, 3 }
        };
        yield return new object[]
        {
            new HashSet<IOfficial>
            {
                new Official()
                {
                    Id = 1, RequiresInquiryFrom = new HashSet<int>() { 2 }
                },
                new Official()
                {
                    Id = 2, RequiresInquiryFrom = new HashSet<int>() { 3, 4 }
                },
                new Official()
                {
                    Id = 3, RequiresInquiryFrom = new HashSet<int>() { 5, 7 }
                }
            },
            new List<int>() { 4, 7, 5, 3, 2, 1 }
        };
        yield return new object[]
        {
            new HashSet<IOfficial>
            {
                new Official()
                {
                    Id = 3, RequiresInquiryFrom = new HashSet<int>() { 2, 4 }
                },
                new Official()
                {
                    Id = 5, RequiresInquiryFrom = new HashSet<int>() { 3 }
                }
            },
            new List<int>() { 4, 2, 3, 5 }
        };
        yield return new object[]
        {
            new HashSet<IOfficial>
            {
                new Official()
                {
                    Id = 2, RequiresInquiryFrom = new HashSet<int>() { 3, 4 }
                },
                new Official()
                {
                    Id = 1, RequiresInquiryFrom = new HashSet<int>() { 2 }
                }
            },
            new List<int>() { 4, 3, 2, 1 }
        };
        yield return new object[]
        {
            new HashSet<IOfficial>
            {
                new Official()
                {
                    Id = 2, RequiresInquiryFrom = new HashSet<int>() { 3, 4 }
                },
                new Official()
                {
                    Id = 1, RequiresInquiryFrom = new HashSet<int>() { 2 }
                },
                new Official()
                {
                    Id = 3, RequiresInquiryFrom = new HashSet<int>() { 5, 4 }
                }
            },
            new List<int>() { 4, 5, 3, 2, 1 }
        };
    }

    public static IEnumerable<object[]> CyclicInquiryPathFinderTestData()
    {
        yield return new object[]
        {
            new HashSet<IOfficial>
            {
                new Official()
                {
                    Id = 1, RequiresInquiryFrom = new HashSet<int>() { 2 }
                },
                new Official()
                {
                    Id = 2, RequiresInquiryFrom = new HashSet<int>() { 1 }
                }
            }
        };
        yield return new object[]
        {
            new HashSet<IOfficial>
            {
                new Official()
                {
                    Id = 1, RequiresInquiryFrom = new HashSet<int>() { 2 }
                },
                new Official()
                {
                    Id = 2, RequiresInquiryFrom = new HashSet<int>() { 3, 4 }
                },
                new Official()
                {
                    Id = 3, RequiresInquiryFrom = new HashSet<int>() { 2, 5 }
                }
            }
        };
    }
}