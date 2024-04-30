using Microsoft.EntityFrameworkCore;
using NFTMarketPlace.Domain.Collections;
using NFTMarketPlace.Domain.Holders;
using NFTMarketPlace.Domain.NFTs;
using NFTMarketPlace.Domain.SeedWork.Events;
using NFTMarketPlace.Domain.Wallets;

namespace NFTMarketPlace.Infrastructure.Database.Context;

public sealed class NFTDbContext : DbContext
{
    public NFTDbContext(DbContextOptions<NFTDbContext> options) : base(options) { }

    public DbSet<StoredEvent> StoredEvents { get; set; }
    public DbSet<Domain.Files.File> Files { get; set; }
    public DbSet<NFT> NFTs { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Holder> Holders { get; set; }
    public DbSet<Wallet> Wallets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NFTDbContext).Assembly);
    }
}
