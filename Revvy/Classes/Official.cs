namespace Revvy;

public class Official : IOfficial, IEquatable<Official>
{
    public bool Equals(Official? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public int Id { get; set; }
    public HashSet<int> RequiresInquiryFrom { get; set; } = new HashSet<int>();

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Official)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, RequiresInquiryFrom.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())));
    }
}