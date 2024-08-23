using AutoMapper;
using MediatR;
using Microservice.Order.History.Api.Data.Repository.Interfaces;
using Microservice.Order.History.Api.Helpers.Exceptions;
using Microservice.Order.History.Api.Helpers.Interfaces;
using Microservice.Order.History.Api.Mediatr.GetOrderHistory.Model;
using Microservice.Order.History.Api.MediatR.GetOrderHistory;

namespace Microservice.Order.Api.MediatR.GetOrderHistory;

public class GetOrderHistoryQueryHandler(IOrderHistoryRepository orderHistoryRepository,
                                  ICustomerHttpAccessor customerHttpAccessor,
                                  ILogger<GetOrderHistoryQueryHandler> logger,
                                  IMapper mapper) : IRequestHandler<GetOrderHistoryRequest, GetOrderHistoryResponse>
{
    private IOrderHistoryRepository _orderHistoryRepository { get; set; } = orderHistoryRepository;
    private ICustomerHttpAccessor _customerHttpAccessor { get; set; } = customerHttpAccessor;
    private IMapper _mapper { get; set; } = mapper;

    private ILogger<GetOrderHistoryQueryHandler> _logger { get; set; } = logger;

    public async Task<GetOrderHistoryResponse> Handle(GetOrderHistoryRequest request, CancellationToken cancellationToken)
    {
        var orderHistory = await _orderHistoryRepository.GetByIdAsync(request.Id, _customerHttpAccessor.CustomerId);
        if (orderHistory == null)
        {
            _logger.LogError($"Order history not found for id - {request.Id}");
            throw new NotFoundException("Order history not found.");
        }

        return new GetOrderHistoryResponse(_mapper.Map<OrderHistory>(orderHistory));
    }
}