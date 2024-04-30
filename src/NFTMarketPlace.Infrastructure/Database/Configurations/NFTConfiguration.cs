using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NFTMarketPlace.Domain.Files;
using NFTMarketPlace.Domain.NFTs;

namespace NFTMarketPlace.Infrastructure.Database.Configurations;

internal sealed class NFTConfiguration : IEntityTypeConfiguration<NFT>
{
    public void Configure(EntityTypeBuilder<NFT> builder)
    {
        builder.ToTable("NFTs", "NFT");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value,
            x => new NFTId(x));

        builder.Property(x => x.URI)
            .IsRequired(true)
            .HasColumnType("nvarchar(1024)");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("nvarchar(50)")
            .HasMaxLength(50);

        builder.Property(x => x.Address)
        .HasColumnType("nvarchar(1024)")
        .IsRequired();

        builder.Property(x => x.Created)
          .IsRequired(true)
          .HasColumnType("datetime");

        builder.Property(x => x.OwnerId)
        .IsRequired(false)
        .HasConversion(x => x.Value,
        x => new Domain.Holders.HolderId(x));

        builder.Property(x => x.CollectionId)
      .IsRequired(false)
      .HasConversion(x => x.Value,
      x => new Domain.Collections.CollectionId(x));

        builder.Property(x => x.FileId)
    .IsRequired(false)
    .HasConversion(x => x.Value,
    x => new FileId(x));


        builder.Property(x => x.ForSell)
            .IsRequired(true)
            .HasColumnType("bit");
    }
}
