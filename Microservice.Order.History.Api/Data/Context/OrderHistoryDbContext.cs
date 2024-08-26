using Microsoft.EntityFrameworkCore;

namespace Microservice.Order.History.Api.Data.Context;

public class OrderHistoryDbContext(DbContextOptions<OrderHistoryDbContext> options) : DbContext(options)
{
    public DbSet<Domain.OrderHistory> OrdersHistory { get; set; }
    public DbSet<Domain.OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.OrderItem>().HasKey(m => new { m.OrderId, m.ProductId });
        modelBuilder.Entity<Domain.OrderHistory>().HasMany(e => e.OrderItems);
    }
}

//add-migration
//update-database

//azurite --silent --location c:\azurite --debug c:\azurite\debug.log