namespace Write.Implementation.Commands.BindRequests;

public class AcceptBindRequestCommand
{
    public AcceptBindRequestCommand(string buildingName, string userToBind)
    {
        BuildingName = buildingName;
        UserToBind = Guid.Parse(userToBind);
    }

    public string BuildingName { get; set; }
    public Guid UserToBind { get; set; }
}