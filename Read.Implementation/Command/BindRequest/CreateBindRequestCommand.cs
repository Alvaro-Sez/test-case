using Read.Implementation.Dto.CustomValidation;

namespace Read.Implementation.Command.BindRequest;

public class CreateBindRequestCommand
{
    
    public CreateBindRequestCommand(string buildingName, Guid userToBind)
    {
        BuildingName = buildingName;
        UserToBind = userToBind;
    }

    public string BuildingName { get; set; }
    public Guid UserToBind { get; set; }
}