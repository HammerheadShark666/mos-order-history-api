using Microservice.Order.History.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Order.History.Api.Data.Context;

public class OrderHistoryDbContext(DbContextOptions<OrderHistoryDbContext> options) : DbContext(options)
{
    public DbSet<Domain.OrderHistory> OrdersHistory { get; set; }
    public DbSet<Domain.OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OrderHistory>()
        .HasMany(e => e.OrderItems)
        .WithOne(e => e.Order)
        .HasForeignKey(e => e.OrderId)
        .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Domain.OrderItem>()
            .HasKey(m => new { m.OrderId, m.ProductId });

        //modelBuilder.Entity<Domain.OrderHistory>().HasMany(e => e.OrderItems);

        modelBuilder.Entity<Domain.OrderHistory>().HasData(DefaultData.GetOrderHistoryDefaultData());
        modelBuilder.Entity<Domain.OrderItem>().HasData(DefaultData.GetOrderItemDefaultData());
    }
}