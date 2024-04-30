using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NFTMarketPlace.Domain.Wallets;

namespace NFTMarketPlace.Infrastructure.Database.Configurations;

internal sealed class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets", "Wallet");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value,
            x => new WalletId(x));


        builder.Property(x=>x.WalletType)
            .HasColumnType("nvarchar(50)")
            .IsRequired()
            .HasConversion(new EnumToStringConverter<WalletType>());


        builder.Property(x => x.Address)
          .HasColumnType("nvarchar(1024)")
          .IsRequired();


        builder.Property(x => x.ChainID)
         .HasColumnType("bigint")
         .IsRequired();


        builder.Property(x => x.Name)
         .HasColumnType("nvarchar(50)")
         .IsRequired();

        builder.Property(x => x.HolderId)
            .HasConversion(x => x.Value,
            x => new Domain.Holders.HolderId(x));
    }
}
