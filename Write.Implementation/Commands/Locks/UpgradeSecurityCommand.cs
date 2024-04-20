namespace Write.Implementation.Commands.Locks;

public class UpgradeSecurityCommand 
{
    public UpgradeSecurityCommand(Guid requestingUserId, string lockId)
    {
        RequestingUserId = requestingUserId;
        LockId = Guid.Parse(lockId);
    }
    public Guid RequestingUserId { get;init; }
    public Guid LockId { get;init; }
}