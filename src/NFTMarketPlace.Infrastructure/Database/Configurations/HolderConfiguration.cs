using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NFTMarketPlace.Domain.Files;
using NFTMarketPlace.Domain.Holders;

namespace NFTMarketPlace.Infrastructure.Database.Configurations;

internal sealed class HolderConfiguration : IEntityTypeConfiguration<Holder>
{
    public void Configure(EntityTypeBuilder<Holder> builder)
    {
        builder.ToTable("Files", "FileManagement");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
            x => x.Value,
            x => new HolderId(x));

        builder.Property(x => x.Firsname)
            .HasMaxLength(50)
            .HasColumnType("nvarchar(50)")
            .IsRequired(false);

        builder.Property(x => x.Lastname)
           .HasMaxLength(50)
           .HasColumnType("nvarchar(50)")
           .IsRequired(false);

        builder.Property(x => x.Email)
           .HasMaxLength(150)
           .HasColumnType("nvarchar(150)")
           .IsRequired(false);

        builder.Property(x => x.Nickname)
         .HasMaxLength(50)
         .HasColumnType("nvarchar(50)")
         .IsRequired(false);

        builder.Property(x => x.Country)
            .HasMaxLength(50)
            .HasColumnType("nvarchar(50)")
            .IsRequired(false);

        builder.Property(x => x.Created)
                  .HasColumnType("datetime")
                  .IsRequired();

        builder.Property(x => x.BirthDate)
                 .HasColumnType("datetime")
                 .IsRequired(false);


        builder.Property(x => x.Blocked)
              .HasColumnType("bit")
              .IsRequired(true);


        builder.Property(x => x.FileId)
            .HasConversion(x => x.Value,
            x => new FileId(x))
            .IsRequired(false);



    }
}
