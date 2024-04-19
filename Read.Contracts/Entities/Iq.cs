namespace Read.Contracts.Entities;

public class Iq
{
    public Guid Id { get; set; }
    // public string BuildingName { get; set; } = string.Empty;
    public List<Lock> Locks { get; set; }
    public string BuildingName { get; set; } = string.Empty;
}