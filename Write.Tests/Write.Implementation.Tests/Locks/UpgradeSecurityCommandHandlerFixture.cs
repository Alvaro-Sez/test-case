using Write.Contacts.Entities;

namespace Write.Tests.Write.Implementation.Tests.Locks;

public class UpgradeSecurityCommandHandlerFixture
{
    public static Guid IqId = new Guid("27a5fb17-150d-4062-acb3-352a1fe08e6d");
    public static Guid FakeLockId = new Guid("02a8d9aa-b69b-47aa-88b3-3692e0539656");
    public static Lock FakeLock = new Lock(FakeLockId){Iq=new Iq()};
    
    public static List<Iq> FakeIqsWithNoLockAssigned = new()
    { 
        new Iq 
        {
            Id = IqId, 
            Locks = new List<Lock>(){new Lock(Guid.NewGuid()) }
        } 
    };
    public static List<Iq> FakeIqsWithLockAssigned = new()
    { 
        new Iq 
        {
            Id = IqId, 
            Locks = new List<Lock>(){new Lock(FakeLockId) }
        } 
    };
    public static User? FakeUserWithNoIq = default(User);
    
    public static User FakeUserWithLowLevelAccess = new User 
    { 
        Id = Guid.NewGuid(), 
        IqAssigned = FakeIqsWithLockAssigned,
        AccessLevel = Access.Low
    };
    
    public static User FakeUserWithHighLevelAccessAndIqAssigned = new User 
    {  
        Id = Guid.NewGuid(), 
        IqAssigned = FakeIqsWithLockAssigned, 
        AccessLevel = Access.High
    };
    public static User FakeUserWithHighLevelAccessNoLockAssigned = new User 
    {  Id = Guid.NewGuid(), 
        IqAssigned = FakeIqsWithNoLockAssigned, 
        AccessLevel = Access.High
    };
} 