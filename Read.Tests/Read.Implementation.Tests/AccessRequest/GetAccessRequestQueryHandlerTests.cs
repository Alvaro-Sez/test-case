using Moq;
using Read.Contracts.Repository;
using Read.Implementation.Dto;
using Read.Implementation.Queries.AccessRequest;
using Read.Implementation.Queries.AccessRequest.Handlers;
using Fixture = Read.Tests.Read.Implementation.Tests.AccessRequest.GetAccessRequestTestFixture;

namespace Read.Tests.Read.Implementation.Tests.AccessRequest;

public class GetAccessRequestQueryHandlerTests
{
    [Fact]
    public async Task Admin_should_Get_all_the_requests()
    {
        var accessRequestRepositoryStub = new Mock<IAccessRequestRepository>();
        var userAccessRepositoryStub = new Mock<IUserAccessRepository>();

        var input = new GetAccessRequest(Fixture.FakeUserRequesting.UserId, true);
        var expectedResponse = new GetAccessRequestsQuery{ Requests = Fixture.FakeAccessRequests };
        
        var sut = new GetAccessRequestQueryHandler(accessRequestRepositoryStub.Object, userAccessRepositoryStub.Object);
        var actualResponse = await sut.HandleAsync(input);
        
    }
}