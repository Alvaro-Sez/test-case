using Read.Contracts.Entities;

namespace Read.Tests.Read.Implementation.Tests.Locks;

public static class OpenLockTestFixture
{
    public static readonly Guid IqId = new("f7278d1e-d6c7-425b-93fd-e4da79468557");
    public static readonly UserAccess? FakeUserWithNoIq = null;

    public static readonly Iq OtherIqWithoutThisLock = new()
        { Id = Guid.NewGuid(), Locks = new List<Lock>() { } };
       
    public static readonly UserAccess FakeUserWithOtherIq = new UserAccess
    {
            UserId = Guid.NewGuid(),
            Iqs = new List<Guid>(1){ OtherIqWithoutThisLock.Id },
            Access = AccessLevel.Low
    };
    
    public static readonly UserAccess FakeUserWithCorrectIqLowAccess = new UserAccess
    {
            UserId = Guid.NewGuid(),
            Iqs = new List<Guid>(1){ IqId },
            Access = AccessLevel.Low
    };

    public static readonly UserAccess FakeUserWithCorrectIqHighAccess = new UserAccess
    {
            UserId = Guid.NewGuid(),
            Iqs = new List<Guid>(1){ IqId },
            Access = AccessLevel.High
    };
    
    public static readonly string LockIdInput = "ee29679a-72c6-4585-9026-d5fcd0ac03e3";
    public static readonly string LockHighSecurityIdInput = "e05d505c-9991-41a0-b3c1-21426a00f027";
    
    public static readonly Lock FakeLock = new() { Id = Guid.Parse(LockIdInput) };
    
    public static readonly Lock FakeLockWithHighSecurity = new() 
    {
        Id = Guid.Parse(LockHighSecurityIdInput), 
        Access = AccessLevel.High
    };
    
    public static readonly IEnumerable<Iq> FakeIqWithHighAccessLock = new List<Iq>()
    {
        new (){ Id = IqId, Locks = new List<Lock>(){ FakeLockWithHighSecurity } }
    };
    
    
    
    
}