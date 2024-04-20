using Microsoft.Extensions.Logging;
using Moq;
using Read.Contracts.Entities;
using Read.Contracts.Events;
using Read.Contracts.Repository;
using Read.Implementation.Dto;
using Read.Implementation.Queries.Locks.Handlers;
using Shared;
using Fixture = Read.Tests.Read.Implementation.Tests.Locks.OpenLockTestFixture;

namespace Read.Tests.Read.Implementation.Tests.Locks;

public class OpenLockQueryHandlerTests
{
    
    [Fact]
    public async Task User_with_no_Iq_assigned_should_get_error()
    {
        var userAccessRepositoryStub = new Mock<IUserAccessRepository>();
        
        userAccessRepositoryStub
            .Setup(c => c.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Fixture.FakeUserWithNoIq);
       
        var input = new OpenLockDto(userId:Guid.NewGuid(), Fixture.LockIdInput);
        var sut = new OpenLockQueryHandler(userAccessRepositoryStub.Object, new Mock<IEventRepository>().Object,new Mock<IIqRepository>().Object, new Mock<ILogger<OpenLockQueryHandler>>().Object);
        
        var result =(await sut.HandleAsync(input));
        var expected = Errors.IqNotAssigned;
        var actual = result.Error;
        
        Assert.Equal(expected, actual);
        Assert.False(result.IsSuccess);
    }
    
    [Fact]
    public async Task User_with_other_Iq_assigned_should_not_open_this_lock()
    {
        var userAccessRepositoryStub = new Mock<IUserAccessRepository>();
        var eventRepositoryStub = new Mock<IEventRepository>();
        var iIqRepositoryStub = new Mock<IIqRepository>();
        
        userAccessRepositoryStub
            .Setup(c => c.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Fixture.FakeUserWithOtherIq);
        
        iIqRepositoryStub
            .Setup(c => c.GetAllByIdAsync(Fixture.FakeUserWithOtherIq.Iqs))
            .ReturnsAsync(new []{ Fixture.OtherIqWithoutThisLock });
        
        eventRepositoryStub.Setup(c => c.SetAsync(It.IsAny<EventRecord>()));
       
        var input = new OpenLockDto(Fixture.FakeUserWithOtherIq.UserId, Fixture.LockIdInput);
        var sut = new OpenLockQueryHandler(userAccessRepositoryStub.Object, eventRepositoryStub.Object, iIqRepositoryStub.Object, new Mock<ILogger<OpenLockQueryHandler>>().Object);
        
        var result =(await sut.HandleAsync(input));
        
        eventRepositoryStub.Verify(c=>c.SetAsync(It.IsAny<EventRecord>()));
        Assert.False(result.Value.Unlock);
    }
   
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { Fixture.FakeUserWithCorrectIqLowAccess, false },
            new object[] { Fixture.FakeUserWithCorrectIqHighAccess, true }
        };
    
    [Theory]
    [MemberData(nameof(Data))]
    public async Task only_high_user_with_correct_iq_bound_can_open_high_access_lock(UserAccess user, bool expectedResult)
    {
        var userAccessRepositoryStub = new Mock<IUserAccessRepository>();
        var eventRepositoryStub = new Mock<IEventRepository>();
        var iIqRepositoryStub = new Mock<IIqRepository>();
        
        userAccessRepositoryStub
            .Setup(c => c.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(user);
        
        iIqRepositoryStub
            .Setup(c => c.GetAllByIdAsync(It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync(Fixture.FakeIqWithHighAccessLock);
        
        eventRepositoryStub.Setup(c => c.SetAsync(It.IsAny<EventRecord>()));
        
        var input = new OpenLockDto(user.UserId, Fixture.LockHighSecurityIdInput);
        var sut = new OpenLockQueryHandler(userAccessRepositoryStub.Object, eventRepositoryStub.Object, iIqRepositoryStub.Object, new Mock<ILogger<OpenLockQueryHandler>>().Object);
        
        var result =(await sut.HandleAsync(input));
        
        eventRepositoryStub.Verify(c=>c.SetAsync(It.IsAny<EventRecord>()));
        Assert.Equal(expectedResult, result.Value.Unlock);
    }
    
    
    [Fact]
    public async Task even_if_event_service_throws_we_respond_ok()
    {
        var userAccessRepositoryStub = new Mock<IUserAccessRepository>();
        var eventRepositoryStub = new Mock<IEventRepository>();
        var iIqRepositoryStub = new Mock<IIqRepository>();
        
        userAccessRepositoryStub
            .Setup(c => c.GetAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Fixture.FakeUserWithCorrectIqHighAccess);
        
        iIqRepositoryStub
            .Setup(c => c.GetAllByIdAsync(It.IsAny<IEnumerable<Guid>>()))
            .ReturnsAsync(Fixture.FakeIqWithHighAccessLock);
        
        eventRepositoryStub
            .Setup(c => c.SetAsync(It.IsAny<EventRecord>()))
            .ThrowsAsync(new Exception());
        
        var input = new OpenLockDto(Fixture.FakeUserWithCorrectIqHighAccess.UserId, Fixture.LockHighSecurityIdInput);
        var sut = new OpenLockQueryHandler(userAccessRepositoryStub.Object, eventRepositoryStub.Object, iIqRepositoryStub.Object, new Mock<ILogger<OpenLockQueryHandler>>().Object);
        
        var result =(await sut.HandleAsync(input));
        
        eventRepositoryStub.Verify(c=>c.SetAsync(It.IsAny<EventRecord>()));
        Assert.True(result.Value.Unlock);
    }
}