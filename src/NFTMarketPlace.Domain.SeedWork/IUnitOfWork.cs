namespace NFTMarketPlace.Domain.SeedWork;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
}
