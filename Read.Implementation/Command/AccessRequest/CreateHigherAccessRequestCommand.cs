namespace Read.Implementation.Command.AccessRequest;

public class CreateHigherAccessRequestCommand
{
    public Guid UserId { get; set; }
    public Guid IqId { get; set; }
}