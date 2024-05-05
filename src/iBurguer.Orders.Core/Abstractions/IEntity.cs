namespace iBurguer.Orders.Core.Abstractions;

public interface IEntity
{
    IReadOnlyCollection<IDomainEvent> Events { get; }

    void ClearEvents();
}