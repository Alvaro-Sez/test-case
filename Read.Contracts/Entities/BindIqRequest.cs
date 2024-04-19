namespace Read.Contracts.Entities;

public class BindIqRequest
{
    
    public BindIqRequest(Guid authorId, string iqBuildingName, Guid id)
    {
        AuthorId = authorId;
        IqBuildingName = iqBuildingName;
        Id = id;
    }

    public BindIqRequest()
    {
    }

    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public string IqBuildingName { get; set; }
}