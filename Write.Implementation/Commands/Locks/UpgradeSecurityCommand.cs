namespace Write.Implementation.Commands.Locks;

public class UpgradeSecurityCommand 
{
    public UpgradeSecurityCommand(string requestingUserId, string lockId)
    {
        RequestingUserId = Guid.Parse(requestingUserId);
        LockId = Guid.Parse(lockId);
    }
    public Guid RequestingUserId { get;init; }
    public Guid LockId { get;init; }
}