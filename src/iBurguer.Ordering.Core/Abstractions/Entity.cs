using System.Collections.ObjectModel;

namespace iBurguer.Ordering.Core.Abstractions;

public abstract class Entity<TId> : IEntity
    where TId : struct
{
    private List<IDomainEvent> events = new();

    public TId Id { get; init; }

    public IReadOnlyCollection<IDomainEvent> Events => events.ToList().AsReadOnly();

    public void ClearEvents() => events.Clear();

    protected void RaiseEvent(IDomainEvent domainEvent)
    {
        if (events is null)
        {
            events = new List<IDomainEvent>();
        }
        
        events.Add(domainEvent);
    }

    public override bool Equals(object? obj)
    {
        if (!(obj is Entity<TId> other)) return false;

        if (ReferenceEquals(this, other)) return true;

        if (GetType() != other.GetType()) return false;

        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => (GetType().ToString() + Id).GetHashCode();
}