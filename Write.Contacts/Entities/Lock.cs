namespace Write.Contacts.Entities;

public class Lock
{
    public Lock(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; init; }
    public Access AccessLevel { get; private set; } = Access.Low;
    public Iq Iq{ get; init; }
    public void UpgradeSecurity()
    {
        AccessLevel = Access.High;
    }
}