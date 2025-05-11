using Challenge.Process.Aiq.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Challenge.Process.Aiq.EntityFramework.Mappings;

public sealed class FavoriteProductMap: IEntityTypeConfiguration<FavoriteProduct>
{
    public void Configure(EntityTypeBuilder<FavoriteProduct> builder)
    {
        builder.ToTable("FavoriteProduct");
        builder.HasKey(x => new {x.CustomerId, x.ProductId});
    }
}