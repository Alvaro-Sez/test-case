namespace Write.Contacts.Entities;

public class Iq
{
    public Iq()
    {
        
    }
    public Iq(string buildingName)
    {
        if(string.IsNullOrWhiteSpace(buildingName))
        {
            throw new ArgumentException();
        }
        BuildingName = buildingName;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; init; }
    public string BuildingName { get; init; }
    // public IEnumerable<Lock> LockPool { get; }
    public IEnumerable<User> Users { get; }
}