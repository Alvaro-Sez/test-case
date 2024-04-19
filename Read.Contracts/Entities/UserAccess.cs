namespace Read.Contracts.Entities;

public class UserAccess
{
    public Guid UserId { get; set; }
    public List<Guid> Iqs { get; set; } = new List<Guid>();
    public string Access { get; set; } = AccessLevel.Low;
}