namespace Read.Data.Models;

public class BindRequest
{
    public BindRequest(string buildingName)
    {
        BuildingName = buildingName;
    }

    public Guid IqId { get; set; }
    public Guid UserRequestingAccessId { get; set; }
    public string BuildingName { get; set; }
}