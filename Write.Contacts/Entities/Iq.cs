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
        Locks = Enumerable.Range(0, 16).Select(c => new Lock(Guid.NewGuid())).ToList();
    }

    public Guid Id { get; init; }
    public string BuildingName { get; init; }
    public ICollection<Lock> Locks { get; init; }
    public ICollection<User> Users { get; init; }
}