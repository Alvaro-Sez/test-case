namespace Read.Implementation.Command.BindRequest;

public class CreateBindRequestCommand
{
    
    public CreateBindRequestCommand(string buildingName, string userToBind)
    {
        BuildingName = buildingName;
        UserToBind = userToBind;
    }

    public string BuildingName { get; set; }
    public string UserToBind { get; set; }
}