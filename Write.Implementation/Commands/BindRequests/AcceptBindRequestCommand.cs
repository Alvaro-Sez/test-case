namespace Write.Implementation.Commands.BindRequests;

public class AcceptBindRequestCommand
{
    public AcceptBindRequestCommand(string buildingName, string userToBind, string authorizer)
    {
        BuildingName = buildingName;
        UserToBind = Guid.Parse(userToBind);
        Authorizer = Guid.Parse(authorizer);
    }

    public string BuildingName { get; set; }
    public Guid Authorizer { get; set; }
    public Guid UserToBind { get; set; }
}