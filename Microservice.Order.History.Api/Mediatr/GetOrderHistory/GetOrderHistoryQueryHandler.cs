using AutoMapper;
using MediatR;
using Microservice.Order.History.Api.Data.Repository.Interfaces;
using Microservice.Order.History.Api.Helpers.Exceptions;
using Microservice.Order.History.Api.Helpers.Interfaces;
using Microservice.Order.History.Api.Mediatr.GetOrderHistory.Model;
using Microservice.Order.History.Api.MediatR.GetOrderHistory;

namespace Microservice.Order.History.Api.Mediatr.GetOrderHistory;

public class GetOrderHistoryQueryHandler(IOrderHistoryRepository orderHistoryRepository,
                                         ICustomerHttpAccessor customerHttpAccessor,
                                         ILogger<GetOrderHistoryQueryHandler> logger,
                                         IMapper mapper) : IRequestHandler<GetOrderHistoryRequest, GetOrderHistoryResponse>
{
    public async Task<GetOrderHistoryResponse> Handle(GetOrderHistoryRequest request, CancellationToken cancellationToken)
    {
        var orderHistory = await orderHistoryRepository.GetByIdAsync(request.Id, customerHttpAccessor.CustomerId);
        if (orderHistory == null)
        {
            logger.LogError("Order history not found for id - {request.Id}", request.Id);
            throw new NotFoundException("Order history not found.");
        }

        return new GetOrderHistoryResponse(mapper.Map<OrderHistory>(orderHistory));
    }
}