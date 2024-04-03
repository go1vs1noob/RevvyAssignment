namespace Revvy;

public class InquiryPathFinder
{
    public InquiryPathFinder(HashSet<IOfficial> officialsDependencySet)
    {
        Officials = RestoreOfficialsFromDependencySet(officialsDependencySet);
    }

    private HashSet<IOfficial> Officials { get; }
    private HashSet<IOfficial> ProcessedOfficials { get; } = new();
    private HashSet<IOfficial> PotentialCycleOfficials { get; } = new();
    public List<int> ResultPath { get; } = new();

    public void FindPath()
    {
        foreach (var official in Officials)
        {
            DepthFirstSearch(official);
        };
    }

    public void PrintSolution()
    {
        foreach (var value in ResultPath)
        {
            Console.Write($"{value} ");
        }
        Console.WriteLine();
    }

    private void DepthFirstSearch(IOfficial startOfficial)
    {
        Stack<IOfficial> stack = new();
        stack.Push(startOfficial);
        PotentialCycleOfficials.Add(startOfficial);

        while (stack.Count > 0)
        {
            var currentOfficial = stack.Peek();
            if (IsProcessed(currentOfficial, stack)) continue;
            var foundUnvisited = ProcessAdjacentOfficials(currentOfficial, stack);
            if (!foundUnvisited)
            {
                MarkAsVisited(currentOfficial);
                stack.Pop();
            }
        }
    }

    private HashSet<IOfficial> RestoreOfficialsFromDependencySet(HashSet<IOfficial> officials)
    {
        HashSet<IOfficial> allOfficials = new();
        foreach (var official in officials)
        {
            allOfficials.Add(official);

            foreach (var adjacentOfficialId in official.RequiresInquiryFrom)
            {
                var adjacentOfficial = officials.FirstOrDefault(o => o.Id == adjacentOfficialId);
                if (adjacentOfficial == null)
                    adjacentOfficial = new Official
                    { Id = adjacentOfficialId, RequiresInquiryFrom = new HashSet<int>() };
                allOfficials.Add(adjacentOfficial);
            }
        }

        return allOfficials;
    }

    private bool IsProcessed(IOfficial currentOfficial, Stack<IOfficial> stack)
    {
        if (ProcessedOfficials.Contains(currentOfficial))
        {
            stack.Pop();
            PotentialCycleOfficials.Remove(currentOfficial);
            return true;
        }

        return false;
    }

    private bool ProcessAdjacentOfficials(IOfficial currentOfficial, Stack<IOfficial> stack)
    {
        var foundUnvisited = false;
        foreach (var prerequisiteId in currentOfficial.RequiresInquiryFrom)
        {
            var adjacentOfficial = Officials.FirstOrDefault(o => o.Id == prerequisiteId);
            if (adjacentOfficial != null && !ProcessedOfficials.Contains(adjacentOfficial))
            {
                if (PotentialCycleOfficials.Contains(adjacentOfficial)) throw new Exception("Cycle found");
                stack.Push(adjacentOfficial);
                PotentialCycleOfficials.Add(adjacentOfficial);
                foundUnvisited = true;
            }
        }

        return foundUnvisited;
    }

    private void MarkAsVisited(IOfficial currentOfficial)
    {
        ProcessedOfficials.Add(currentOfficial);
        ResultPath.Add(currentOfficial.Id);
        PotentialCycleOfficials.Remove(currentOfficial);
    }
}