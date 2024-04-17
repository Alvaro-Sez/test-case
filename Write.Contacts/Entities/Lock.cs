namespace Write.Contacts.Entities;

public class Lock
{
    public Lock(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; init; }
    public Access AccessRequired { get; private set; } = Access.Low;
    public void UpgradeSecurity()
    {
        AccessRequired = Access.High;
    }
}