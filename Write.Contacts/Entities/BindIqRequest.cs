namespace Write.Contacts.Entities;

public class BindIqRequest
{
    public BindIqRequest()
    {
        
    }
    public Guid Id { get; set; } 
    public Guid AuthorId { get; set; }
    public Guid IqId { get; set; }
}