namespace Read.Implementation.Events;

public class BindIqRequestAccepted
{
    public BindIqRequestAccepted(string authorId, string iqId)
    {
        AuthorId = authorId;
        IqId = iqId;
    }

    public BindIqRequestAccepted()
    {
        
    }

    public string AuthorId { get; set; }
    public string IqId { get; set; }
}