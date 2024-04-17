namespace Read.Contracts.Entities;

public class Iq
{
    public Iq(string buildingName)
    {
        BuildingName = buildingName;
    }
    public string BuildingName { get; init; }
}