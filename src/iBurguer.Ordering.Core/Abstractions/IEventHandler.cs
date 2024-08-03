namespace iBurguer.Ordering.Core.Abstractions
{
    public interface IEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        Task Handle(TEvent evt, CancellationToken cancellation);
    }
}
