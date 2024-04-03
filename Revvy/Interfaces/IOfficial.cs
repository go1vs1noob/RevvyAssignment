namespace Revvy;

public interface IOfficial
{
    // We'll say Ids never repeat
    public int Id { get; set; }

    public HashSet<int> RequiresInquiryFrom { get; set; }
}