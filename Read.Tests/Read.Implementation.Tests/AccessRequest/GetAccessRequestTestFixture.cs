using Read.Contracts.Entities;

namespace Read.Tests.Read.Implementation.Tests.AccessRequest;

public static class GetAccessRequestTestFixture
{

    private static readonly Guid FakeIqId = new Guid("437f354f-58ab-4ddd-bccd-ad619f600610");
    
    public static readonly UserAccess FakeUserRequesting = new UserAccess
    {
            UserId = Guid.NewGuid(),
            Iqs = new List<Guid>(1){ Guid.NewGuid() }
    };
    
   public static readonly UserAccess FakeUserRequestingWithHighAccessToOneIq = new UserAccess
    {
            UserId = Guid.NewGuid(),
            Iqs = new List<Guid>(1){ FakeIqId },
            Access = AccessLevel.High
    };
    public static readonly UserAccess FakeUserRequestingWithNoAccessToThisIq = new UserAccess
    {
            UserId = Guid.NewGuid(),
            Iqs = new List<Guid>(1){ Guid.NewGuid() },
            Access = AccessLevel.High
    };
    public static readonly UserAccess FakeLowAccessUserRequesting= new UserAccess
    {
            UserId = Guid.NewGuid(),
            Iqs = new List<Guid>(1){ Guid.NewGuid() },
            Access = AccessLevel.Low
    };
    
    public static readonly List<AccessLevelRequest> FakeAccessRequests = Enumerable
        .Range(0, 6)
        .Select(c => new AccessLevelRequest{ IqId = Guid.NewGuid(), UserId = Guid.NewGuid() })
        .Concat(new []{new AccessLevelRequest{IqId = FakeIqId, UserId = FakeUserRequesting.UserId }})
        .ToList();
}
