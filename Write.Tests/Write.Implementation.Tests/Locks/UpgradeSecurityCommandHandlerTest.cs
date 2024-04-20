using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Moq;
using Shared;
using Write.Contacts.Entities;
using Write.Contacts.Repository;
using Write.Implementation.Commands.Locks;
using Write.Implementation.Commands.Locks.Handlers;
using Fixture = Write.Tests.Write.Implementation.Tests.Locks.UpgradeSecurityCommandHandlerFixture;
namespace Write.Tests.Write.Implementation.Tests.Locks;

public class UpgradeSecurityCommandHandlerTest
{
    
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { Fixture.FakeUserWithNoIq, Errors.IqNotAssigned },
            new object[] { Fixture.FakeUserWithLowLevelAccess, Errors.NoLevelAccess }
        };
    
    [Theory]
    [MemberData(nameof(Data))]
    public async Task Return_error_when_user_has_no_iq_assigned_or_no_level_access(User? user, Error expectedResult)
    {
        var userRepositoryStub = new Mock<IUserRepository>();
        var lockRepositoryStub = new Mock<ILockRepository>();
        var unitOfWorkStub = new Mock<IUnitOfWork>();
        var capPublisherStub = new Mock<ICapPublisher>();
        
        userRepositoryStub
            .Setup(c => c.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(user);
        
        
        var input = new UpgradeSecurityCommand(Guid.NewGuid(), Fixture.FakeLockId.ToString());
        var sut = new UpgradeSecurityCommandHandler(lockRepositoryStub.Object, userRepositoryStub.Object, unitOfWorkStub.Object, capPublisherStub.Object, new Mock<ILogger<UpgradeSecurityCommandHandler>>().Object);
        var result = await sut.HandleAsync(input);

        var actualResult = result.Error;
        
        Assert.False(result.IsSuccess);
        Assert.Equal(expectedResult, actualResult);
        unitOfWorkStub.Verify(c=>c.SaveChangesAsync(),Times.Never);
    }
    
    [Fact]
    public async Task Should_return_ok_when_user_has_high_lvl_access_and_this_IQ()
    {
        var userRepositoryStub = new Mock<IUserRepository>();
        var lockRepositoryStub = new Mock<ILockRepository>();
        var unitOfWorkStub = new Mock<IUnitOfWork>();
        var capPublisherStub = new Mock<ICapPublisher>();
        
        userRepositoryStub
            .Setup(c => c.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Fixture.FakeUserWithHighLevelAccessAndIqAssigned);

        lockRepositoryStub
            .Setup(c => c.GetByIdAsync(Fixture.FakeLockId))
            .ReturnsAsync(Fixture.FakeLock);
        
        var input = new UpgradeSecurityCommand(Guid.NewGuid(), Fixture.FakeLockId.ToString());
        var sut = new UpgradeSecurityCommandHandler(lockRepositoryStub.Object, userRepositoryStub.Object, unitOfWorkStub.Object, capPublisherStub.Object, new Mock<ILogger<UpgradeSecurityCommandHandler>>().Object);
        
        var result = await sut.HandleAsync(input);
        
        Assert.Equal(Access.High, Fixture.FakeLock.AccessLevel);
        Assert.True(result.IsSuccess);
        unitOfWorkStub.Verify(c=>c.SaveChangesAsync(),Times.Once);
    }
    [Fact]
    public async Task Should_return_error_when_lock_not_exist()
    {
        var userRepositoryStub = new Mock<IUserRepository>();
        var lockRepositoryStub = new Mock<ILockRepository>();
        var unitOfWorkStub = new Mock<IUnitOfWork>();
        var capPublisherStub = new Mock<ICapPublisher>();
        
        userRepositoryStub
            .Setup(c => c.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Fixture.FakeUserWithHighLevelAccessAndIqAssigned);

        lockRepositoryStub
            .Setup(c => c.GetByIdAsync(Fixture.FakeLockId))
            .ReturnsAsync(default(Lock));
        
        var input = new UpgradeSecurityCommand(Guid.NewGuid(), Fixture.FakeLockId.ToString());
        var sut = new UpgradeSecurityCommandHandler(lockRepositoryStub.Object, userRepositoryStub.Object, unitOfWorkStub.Object, capPublisherStub.Object, new Mock<ILogger<UpgradeSecurityCommandHandler>>().Object);
        var result = await sut.HandleAsync(input);
        var expected = Errors.LockNotExist;
        var actual = result.Error;
        
        Assert.False(result.IsSuccess);
        Assert.Equal(expected, actual);
        unitOfWorkStub.Verify(c=>c.SaveChangesAsync(),Times.Never);
    }
}