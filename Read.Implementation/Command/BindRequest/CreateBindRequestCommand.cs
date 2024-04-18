namespace Read.Implementation.Command.BindRequest;

public class CreateBindRequestCommand
{
    
    public CreateBindRequestCommand(string buildingName, string userToBind)
    {
        if(!Guid.TryParse(userToBind, out var id))
        {
            throw new ArgumentException("Invalid Id from Idp service");
        }
        BuildingName = buildingName;
        UserToBind = id; // can be parsed safely ? because comes from the idp, that works with guid TODO
    }

    public string BuildingName { get; set; }
    public Guid UserToBind { get; set; }
}