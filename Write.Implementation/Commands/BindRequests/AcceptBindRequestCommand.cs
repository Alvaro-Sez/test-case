namespace Write.Implementation.Commands.BindRequests;

public class AcceptBindRequestCommand
{
    public AcceptBindRequestCommand(string buildingName, Guid userToBind)
    {
        BuildingName = buildingName;
        UserToBind = userToBind;
    }

    public string BuildingName { get; set; }
    public Guid UserToBind { get; set; }
}