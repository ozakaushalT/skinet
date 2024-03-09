using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(_ => _.ShipToAddress, a =>
            {
                a.WithOwner(); // if owner entity is deleted, owned entity will also be deleted
            });
            builder.Property(_ => _.OrderStatus).HasConversion(_ => _.ToString()
            , _ => (OrderStatus)Enum.Parse(typeof(OrderStatus), _));

            builder.HasMany(_ => _.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(_ => _.ShipToAddress).IsRequired();
        }
    }
}