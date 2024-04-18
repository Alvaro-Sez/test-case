namespace Read.Data.Models;

public class BindRequestModel
{
    public Guid IqId { get; set; }
    public Guid UserRequestingAccessId { get; set; }
    public string BuildingName { get; set; } = string.Empty;
}