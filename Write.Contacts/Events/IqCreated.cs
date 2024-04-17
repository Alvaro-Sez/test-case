namespace Write.Contacts.Events;

public class IqCreated
{
    public IqCreated(string buildingName)
    {
        BuildingName = buildingName;
    }

    public string BuildingName { get; set; }
}