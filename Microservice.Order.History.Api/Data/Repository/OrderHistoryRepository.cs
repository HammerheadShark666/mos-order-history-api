using Microservice.Order.History.Api.Data.Context;
using Microservice.Order.History.Api.Data.Repository.Interfaces;
using Microservice.Order.History.Api.Helpers.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Order.History.Api.Data.Repository;

public class OrderHistoryRepository(IDbContextFactory<OrderHistoryDbContext> dbContextFactory) : IOrderHistoryRepository
{
    public async Task Delete(Domain.OrderHistory orderHistory)
    {
        using var db = dbContextFactory.CreateDbContext();

        db.OrdersHistory.Remove(orderHistory);
        await db.SaveChangesAsync();
    }

    public async Task<Domain.OrderHistory> GetByIdAsync(Guid id, Guid customerId)
    {
        await using var db = await dbContextFactory.CreateDbContextAsync();

        var orderHistory = await db.OrdersHistory
                        .Where(o => o.Id.Equals(id) && o.CustomerId.Equals(customerId))
                        .Include(e => e.OrderItems)
                        .SingleOrDefaultAsync() ?? throw new NotFoundException("Order history not found.");

        return orderHistory;
    }

    public async Task<List<Domain.OrderHistory>> SearchByDateAsync(DateOnly date)
    {
        await using var db = await dbContextFactory.CreateDbContextAsync();
        return await db.OrdersHistory
                        .Where(o => o.Created.Equals(date))
                        .Include(e => e.OrderItems)
                        .OrderBy(e => e.Created)
                        .ToListAsync();
    }
}