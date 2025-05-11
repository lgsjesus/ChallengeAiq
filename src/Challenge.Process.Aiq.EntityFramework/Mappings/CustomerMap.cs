using Challenge.Process.Aiq.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Process.Aiq.EntityFramework.Mappings;

public sealed class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
       builder.ToTable("Customer");
       builder.HasKey(x => x.Id);
       builder.Property(x => x.Name).IsRequired();
       builder.Property(x => x.Email).IsRequired();
       builder.HasIndex(x=>x.Email).IsUnique();
       builder.HasMany(c=> c.FavoriteProducts)
           .WithOne(f => f.Customer).HasForeignKey(f => f.CustomerId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}