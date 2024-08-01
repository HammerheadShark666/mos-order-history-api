using AutoMapper;
using MediatR;
using Microservice.Order.History.Api.Data.Repository.Interfaces;
using Microservice.Order.History.Api.Helpers.Exceptions;
using Microservice.Order.History.Api.Helpers.Interfaces;
using Microservice.Order.History.Api.MediatR.GetOrderHistory;
using Microservice.Order.History.Api.Mediatr.GetOrderHistory.Model;

namespace Microservice.Order.Api.MediatR.GetOrderHistory;

public class GetOrderQueryHandler(IOrderHistoryRepository orderHistoryRepository, 
                                  ICustomerHttpAccessor customerHttpAccessor,
                                  IMapper mapper) : IRequestHandler<GetOrderHistoryRequest, GetOrderHistoryResponse>
{
    private IOrderHistoryRepository _orderHistoryRepository { get; set; } = orderHistoryRepository;
    private ICustomerHttpAccessor _customerHttpAccessor { get; set; } = customerHttpAccessor;
    private IMapper _mapper { get; set; } = mapper;
     
    public async Task<GetOrderHistoryResponse> Handle(GetOrderHistoryRequest request, CancellationToken cancellationToken)
    {  
        var orderHistory = await _orderHistoryRepository.GetByIdAsync(request.Id, _customerHttpAccessor.CustomerId);
        if (orderHistory == null)
            throw new NotFoundException($"Order history not found for id - {request.Id}");
          
        return new GetOrderHistoryResponse(_mapper.Map<OrderHistory>(orderHistory)); 
    }
}