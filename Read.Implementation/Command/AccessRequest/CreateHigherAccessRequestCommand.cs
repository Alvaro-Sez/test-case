namespace Read.Implementation.Command.AccessRequest;

public class CreateHigherAccessRequestCommand
{
    public CreateHigherAccessRequestCommand(Guid userId, string iqId)
    {
        UserId = userId;
        IqId = Guid.Parse(iqId);
    }
    public Guid UserId { get; set; }
    public Guid IqId { get; set; }
}