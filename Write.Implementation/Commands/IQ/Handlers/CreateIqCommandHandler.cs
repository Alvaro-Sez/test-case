using DotNetCore.CAP;
using Shared;
using Write.Contacts.Commands;
using Write.Contacts.Repository;

namespace Write.Implementation.Commands.IQ.Handlers;

public class CreateIqCommandHandler: ICommandHandler<CreateIQCommand>
{
    private readonly IIqRepository _iqRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICapPublisher _capPublisher;
    public CreateIqCommandHandler(IUnitOfWork unitOfWork, IIqRepository iqRepository, ICapPublisher capPublisher)
    {
        _unitOfWork = unitOfWork;
        _iqRepository = iqRepository;
        _capPublisher = capPublisher;
    }

    public Task<Result> HandleAsync(CreateIQCommand command)
    {
        throw new NotImplementedException();
    }
}