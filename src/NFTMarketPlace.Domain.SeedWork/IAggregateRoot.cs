using NFTMarketPlace.Domain.SeedWork.Events;

namespace NFTMarketPlace.Domain.SeedWork;
public interface IAggregateRoot
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}
