namespace Read.Contracts.Entities;

public class UserAccess
{
    public Guid UserId { get; set; }
    public List<Iq> Iqs { get; set; } = new List<Iq>();
}