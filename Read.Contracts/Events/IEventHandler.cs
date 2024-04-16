namespace Read.Contracts.Events;

public interface IEventHandler<TEvent> 
{
    public void Handle(TEvent @event);
}