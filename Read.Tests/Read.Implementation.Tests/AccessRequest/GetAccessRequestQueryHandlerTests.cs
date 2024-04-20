using System.Text.Json;
using Moq;
using Read.Contracts.Entities;
using Read.Contracts.Repository;
using Read.Implementation.Dto;
using Read.Implementation.Queries.AccessRequest;
using Read.Implementation.Queries.AccessRequest.Handlers;
using Shared;
using Fixture = Read.Tests.Read.Implementation.Tests.AccessRequest.GetAccessRequestTestFixture;

namespace Read.Tests.Read.Implementation.Tests.AccessRequest;

public class GetAccessRequestQueryHandlerTests
{
    [Fact]
    public async Task Admin_should_Get_all_high_access_requests()
    {
        var accessRequestRepositoryStub = BuildAccessRequestRepositoryStub(); 
        
        var userAccessRepositoryStub = new Mock<IUserAccessRepository>();
        userAccessRepositoryStub
            .Setup(c => c.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Fixture.FakeUserRequesting);
        
        var input = new GetAccessRequest(Fixture.FakeUserRequesting.UserId, true);
        var sut = new GetAccessRequestQueryHandler(accessRequestRepositoryStub.Object, userAccessRepositoryStub.Object);
        
        var actualResponse = (await sut.HandleAsync(input)).Value;
        var expectedResponse = new GetAccessRequestsQuery{Requests = Fixture.FakeAccessRequests};

        var expected = JsonSerializer.Serialize(expectedResponse);
        var actual = JsonSerializer.Serialize(actualResponse);
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public async Task No_admin_User_with_high_access_with_Iq_bound_to_one_request_should_get_only_that_request()
    {
        var userAccessRepositoryStub = new Mock<IUserAccessRepository>();
        
        var accessRequestRepositoryStub = BuildAccessRequestRepositoryStub();
        userAccessRepositoryStub
            .Setup(c => c.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Fixture.FakeUserRequestingWithHighAccessToOneIq);
        
        var input = new GetAccessRequest(Fixture.FakeUserRequestingWithHighAccessToOneIq.UserId, false);
        var sut = new GetAccessRequestQueryHandler(accessRequestRepositoryStub.Object, userAccessRepositoryStub.Object);
        
        var actualResponse = (await sut.HandleAsync(input)).Value;
        
        Assert.Single(actualResponse.Requests);
    }
    
    [Fact]
    public async Task User_high_access_without_Iq_bound_should_get_empty()
    {
        var userAccessRepositoryStub = new Mock<IUserAccessRepository>();
        
        var accessRequestRepositoryStub = BuildAccessRequestRepositoryStub();
        userAccessRepositoryStub
            .Setup(c => c.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Fixture.FakeUserRequestingWithNoAccessToThisIq);
        
        var input = new GetAccessRequest(Fixture.FakeUserRequestingWithNoAccessToThisIq.UserId, false);
        var sut = new GetAccessRequestQueryHandler(accessRequestRepositoryStub.Object, userAccessRepositoryStub.Object);
        
        var actualResponse = (await sut.HandleAsync(input)).Value;
        
        Assert.Empty(actualResponse.Requests);
    }
    [Fact]
    public async Task User_low_access_without_Iq_bound_should_get_error()
    {
        var userAccessRepositoryStub = new Mock<IUserAccessRepository>();
        
        var accessRequestRepositoryStub = BuildAccessRequestRepositoryStub();
        userAccessRepositoryStub
            .Setup(c => c.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Fixture. FakeLowAccessUserRequesting);
        
        var input = new GetAccessRequest(Fixture. FakeLowAccessUserRequesting.UserId, false);
        var sut = new GetAccessRequestQueryHandler(accessRequestRepositoryStub.Object, userAccessRepositoryStub.Object);

        var actualResponse = (await sut.HandleAsync(input));
        var expectedError = Errors.NoLevelAccess;
        
        Assert.Equal( expectedError, actualResponse.Error);
    }
    private static Mock<IAccessRequestRepository> BuildAccessRequestRepositoryStub()
    {
        var stub = new Mock<IAccessRequestRepository>();
        stub 
            .Setup(c => c.GetAllAsync())
            .ReturnsAsync(Fixture.FakeAccessRequests);
        return stub;
    }
}