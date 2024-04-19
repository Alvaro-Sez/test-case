namespace Write.Implementation.Commands.Access;

public class AcceptHigherAccessCommand
{
    public Guid UserRequestingId { get; set; }
    public Guid UserAcceptingId { get; set; }
    public bool IsAdmin { get; set; } = false;
}