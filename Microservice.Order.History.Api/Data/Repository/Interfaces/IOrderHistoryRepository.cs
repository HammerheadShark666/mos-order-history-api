namespace Microservice.Order.History.Api.Data.Repository.Interfaces;

public interface IOrderHistoryRepository
{
    Task Delete(Domain.OrderHistory orderHistory);
    Task<Domain.OrderHistory> GetByIdAsync(Guid id, Guid customerId);
    Task<List<Domain.OrderHistory>> SearchByDateAsync(DateOnly date);
}