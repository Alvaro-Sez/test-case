using Read.Contracts.Entities;

namespace Read.Tests.Read.Implementation.Tests.AccessRequest;

public static class GetAccessRequestTestFixture
{
    public static List<AccessLevelRequest> FakeAccessRequests = Enumerable
        .Range(0, 6)
        .Select(c => new AccessLevelRequest{ IqId = Guid.NewGuid(), UserId = Guid.NewGuid() })
        .Concat(new []{new AccessLevelRequest{IqId = FakeIqId, UserId = FakeUserRequesting.UserId }})
        .ToList();

    private static Guid FakeIqId = new Guid("437f354f-58ab-4ddd-bccd-ad619f600610");
    
    public static readonly UserAccess FakeUserRequesting = new UserAccess
    {
            UserId = Guid.NewGuid(),
            Iqs = new List<Guid>(1){ FakeIqId }
    };
    
   public static readonly UserAccess FakeUserAcceptingWithHighAccessToThisIq = new UserAccess
    {
            UserId = Guid.NewGuid(),
            Iqs = new List<Guid>(1){ FakeIqId },
            Access = AccessLevel.High
    };
    public static readonly UserAccess FakeUserAcceptingWithNoAccessToThisIq = new UserAccess
    {
            UserId = Guid.NewGuid(),
            Iqs = new List<Guid>(1){ Guid.NewGuid() },
            Access = AccessLevel.High
    };
}
