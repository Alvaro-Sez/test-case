namespace Read.Contracts.Entities;

public class BindIqRequest
{
    
    public BindIqRequest(string authorId, string iqBuildingName)
    {
        AuthorId = authorId;
        IqBuildingName = iqBuildingName;
    }

    public BindIqRequest()
    {
        
    }

    public string AuthorId { get; set; }
    public string IqBuildingName { get; set; }
}