namespace Write.Contacts.Events;

public class IqCreatedEvent
{
    public IqCreatedEvent(string buildingName)
    {
        BuildingName = buildingName;
    }

    public string BuildingName { get; set; }
}