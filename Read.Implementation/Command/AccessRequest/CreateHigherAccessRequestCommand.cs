namespace Read.Implementation.Command.AccessRequest;

public class CreateHigherAccessRequestCommand
{
    public CreateHigherAccessRequestCommand(string userId, string iqId)
    {
        UserId = Guid.Parse(userId);
        IqId = Guid.Parse(iqId);
    }
    public Guid UserId { get; set; }
    public Guid IqId { get; set; }
}