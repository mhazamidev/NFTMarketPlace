using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NFTMarketPlace.Domain.Collections;

namespace NFTMarketPlace.Infrastructure.Database.Configurations;

internal sealed class CollectionConfiguration : IEntityTypeConfiguration<Collection>
{
    public void Configure(EntityTypeBuilder<Collection> builder)
    {
        builder.ToTable("Collections", "NFT");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value,
            x => new CollectionId(x));

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired(true)
            .HasColumnType("nvarchar(50)");

        builder.Property(x => x.IsActive)
            .IsRequired(true)
            .HasColumnType("bit");

        builder.Property(x => x.Created)
            .IsRequired(true)
            .HasColumnType("datetime");

        builder.Property(x => x.FileId)
            .IsRequired(false)
            .HasConversion(x => x.Value,
            x => new Domain.Files.FileId(x));

    }
}
