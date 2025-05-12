using Challenge.Process.Aiq.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Process.Aiq.EntityFramework.Mappings;

internal sealed class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Password).IsRequired();
        builder.HasIndex(x=>x.Email).IsUnique();
    }
}