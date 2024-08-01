using MediatR;

namespace Microservice.Order.History.Api.MediatR.GetOrderHistory;

public record GetOrderHistoryRequest(Guid Id) : IRequest<GetOrderHistoryResponse>;