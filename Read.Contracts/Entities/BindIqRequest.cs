using System.Runtime.Serialization;

namespace Read.Contracts.Entities;

public class BindIqRequest
{
    
    public BindIqRequest(Guid authorId, string iqBuildingName)
    {
        AuthorId = authorId;
        IqBuildingName = iqBuildingName;
    }

    public BindIqRequest()
    {
    }

    public Guid AuthorId { get; set; }
    public string IqBuildingName { get; set; }
}