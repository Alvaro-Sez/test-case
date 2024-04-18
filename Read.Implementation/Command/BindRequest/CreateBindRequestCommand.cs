namespace Read.Implementation.Command.BindRequest;

public class CreateBindRequestCommand
{
    
    public CreateBindRequestCommand(string buildingName, string userToBind)
    {
        BuildingName = buildingName;
        UserToBind = Guid.Parse(userToBind); // can be parsed safely ? because comes from the idp, that works with guid TODO
    }

    public string BuildingName { get; set; }
    public Guid UserToBind { get; set; }
}