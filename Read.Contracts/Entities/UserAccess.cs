namespace Read.Contracts.Entities;

public class UserAccess
{
    public UserAccess()
    {
    }

    public Guid UserId { get; set; }
    public IEnumerable<Iq> Iqs{ get; set; }   
}