namespace Revvy;

public class NumberSumChecker
{
    public NumberSumChecker(int target, int[] candidates)
    {
        Target = target;
        Candidates = candidates;
        Array.Sort(Candidates);
    }

    private HashSet<int> CandidatesInUse { get; } = new();
    public int Target { get; }
    public int[] Candidates { get; }

    public bool TargetCanBeSumOfCandidates()
    {
        return CanBeConstructed(Target);
    }

    private bool CanBeConstructed(int target)
    {
        if (target == 0) return true;
        for (var candidateIndex = 0; candidateIndex < Candidates.Length; candidateIndex++)
        {
            var candidate = Candidates[candidateIndex];
            if (Candidates[candidateIndex] <= target && !CandidatesInUse.Contains(candidateIndex))
            {
                CandidatesInUse.Add(candidateIndex);
                if (CanBeConstructed(target - candidate)) return true;
                CandidatesInUse.Remove(candidateIndex);
            }
        }

        return false;
    }
}