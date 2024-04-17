using Shared;
using Write.Contacts.Commands;

namespace Write.Implementation.Commands.BindRequests;

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