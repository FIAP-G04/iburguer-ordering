namespace iBurguer.Ordering.Core.Abstractions;

public interface IEntity
{
    IReadOnlyCollection<IDomainEvent> Events { get; }

    void ClearEvents();
}