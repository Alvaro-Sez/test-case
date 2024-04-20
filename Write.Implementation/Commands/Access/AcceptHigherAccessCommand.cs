namespace Write.Implementation.Commands.Access;

public class AcceptHigherAccessCommand
{
    public AcceptHigherAccessCommand(string userRequestingId, Guid userAcceptingId, bool isAdmin)
    {
        UserAcceptingId = userAcceptingId;
        UserRequestingId = Guid.Parse(userRequestingId);
        IsAdmin = isAdmin;
    }
    public Guid UserRequestingId { get; set; }
    public Guid UserAcceptingId { get; set; }
    public bool IsAdmin { get; set; } = false;
}