using Shared;
using Write.Contacts.Commands;

namespace Write.Implementation.Commands.BindRequests;

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