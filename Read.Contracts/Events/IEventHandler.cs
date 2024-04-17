namespace Read.Contracts.Events;

public interface IEventHandler<TEvent> 
{
    public Task Handle(TEvent @event);
}