namespace Read.Implementation.Queries.IQ;

public class GetIqsQuery
{
    public string IqBuildingName { get; set; } = string.Empty;
    public Guid IqId { get; set; }
}