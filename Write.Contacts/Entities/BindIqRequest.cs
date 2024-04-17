using System.Security.Cryptography.X509Certificates;

namespace Write.Contacts.Entities;

public class BindIqRequest
{
    public BindIqRequest(Guid authorId, Guid iqId, Guid id)
    {
        AuthorId = authorId;
        IqId = iqId;
        Id = id;
    }

    public BindIqRequest()
    {
        
    }

    public Guid Id { get; set; } 
    public Guid AuthorId { get; set; }
    public Guid IqId { get; set; }
}