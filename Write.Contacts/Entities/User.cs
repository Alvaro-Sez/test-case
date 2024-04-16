namespace Write.Contacts.Entities;

public class User
{
    public User()
    {
        
    }
    public User(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; init; }
    public List<Iq> IqAssigned { get; set; } = new();
    public Access AccessLevel { get; set; } = Access.None;
}