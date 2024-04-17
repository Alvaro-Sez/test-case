namespace Write.Contacts.Entities;

public class Iq
{
    public Iq()
    {
        
    }
    public Iq(string buildingName, Guid id)
    {
        if(string.IsNullOrWhiteSpace(buildingName))
        {
            throw new ArgumentException();
        }
        BuildingName = buildingName;
        Id = id;
    }

    public Guid Id { get; init; }
    public string BuildingName { get; init; }
    public ICollection<Lock> Locks { get; init; }
    public ICollection<User> Users { get; }
}