namespace Write.Implementation.Commands.IQ;

public class CreateIQCommand 
{
    public CreateIQCommand(string buildingName)
    {
        BuildingName = buildingName;
    }

    public string BuildingName { get; init; }
}