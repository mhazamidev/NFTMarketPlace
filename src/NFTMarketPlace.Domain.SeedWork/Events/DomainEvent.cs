using MediatR;

namespace NFTMarketPlace.Domain.SeedWork.Events;

public interface IDomainEvent : INotification
{
    DateTime CreatedAt { get; }
}
public abstract record class DomainEvent : Message, IDomainEvent
{
    public DateTime CreatedAt { get; init; }

    public DomainEvent()
    {
        CreatedAt = DateTime.Now;
    }
}
