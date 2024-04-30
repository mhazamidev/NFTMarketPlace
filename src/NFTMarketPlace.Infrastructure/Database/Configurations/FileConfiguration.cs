using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NFTMarketPlace.Domain.Files;

namespace NFTMarketPlace.Infrastructure.Database.Configurations;

internal sealed class FileConfiguration : IEntityTypeConfiguration<Domain.Files.File>
{
    public void Configure(EntityTypeBuilder<Domain.Files.File> builder)
    {
        builder.ToTable("Files", "FileManagement");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
            v => v.Value,
            v => new FileId(v));

        builder.Property(x => x.FileName)
          .HasColumnType("nvarchar(100)")
          .HasMaxLength(100)
          .IsRequired();

        builder.Property(x => x.Content)
            .HasColumnType("varbinary(MAX)")
            .IsRequired();

        builder.Property(x => x.ContentType)
            .HasColumnType("nvarchar(50)")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Extension)
            .HasColumnType("nvarchar(10)")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(x => x.Size)
            .HasColumnType("tinyint")
            .IsRequired();



}
}
