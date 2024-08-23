using Microservice.Order.History.Api.Data.Contexts;
using Microservice.Order.History.Api.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Order.History.Api.Data.Repository;

public class OrderHistoryRepository(IDbContextFactory<OrderHistoryDbContext> dbContextFactory) : IOrderHistoryRepository
{
    public IDbContextFactory<OrderHistoryDbContext> _dbContextFactory { get; set; } = dbContextFactory;

    public async Task Delete(Domain.OrderHistory orderHistory)
    {
        using var db = _dbContextFactory.CreateDbContext();

        db.OrdersHistory.Remove(orderHistory);
        await db.SaveChangesAsync();
    }

    public async Task<Domain.OrderHistory> GetByIdAsync(Guid id, Guid customerId)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        return await db.OrdersHistory
                        .Where(o => o.Id.Equals(id) && o.CustomerId.Equals(customerId))
                        .Include(e => e.OrderItems)
                        .SingleOrDefaultAsync();
    }

    public async Task<List<Domain.OrderHistory>> SearchByDateAsync(DateOnly date)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        return await db.OrdersHistory
                        .Where(o => o.Created.Equals(date))
                        .Include(e => e.OrderItems)
                        .OrderBy(e => e.Created)
                        .ToListAsync();
    }
}