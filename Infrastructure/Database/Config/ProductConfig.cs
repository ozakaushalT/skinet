using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(_ => _.Id).IsRequired();
            builder.Property(_ => _.Name).IsRequired().HasMaxLength(1000);
            builder.Property(_ => _.Description).IsRequired();
            // builder.Property(_ => _.PictureURL).IsRequired();
            builder.Property(_ => _.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(_ => _.ProductBrand).WithMany()
            .HasForeignKey(_ => _.ProductBrandId);
            builder.HasOne(_ => _.ProductType).WithMany().HasForeignKey(_ => _.ProductTypeId);
        }
    }
}