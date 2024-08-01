using AutoMapper;
using Microservice.Order.History.Api.Mediatr.GetOrderHistory.Model;

namespace Microservice.Order.History.Api.MediatR.GetOrderHistory;

public class GetOrderHistoryMapper : Profile
{ 
    public GetOrderHistoryMapper()
    {
        base.CreateMap<Api.Domain.OrderHistory, OrderHistory>()
            .ForMember(m => m.Address, o => o.MapFrom(s => s)); 

        base.CreateMap<Api.Domain.OrderItem, OrderItemHistory>(); 

        base.CreateMap<Api.Domain.OrderHistory, OrderHistoryAddress>()
            .ForMember(m => m.AddressLine1, o => o.MapFrom(s => s.AddressLine1))
            .ForMember(m => m.AddressLine2, o => o.MapFrom(s => s.AddressLine2))
            .ForMember(m => m.AddressLine3, o => o.MapFrom(s => s.AddressLine3))
            .ForMember(m => m.TownCity, o => o.MapFrom(s => s.TownCity))
            .ForMember(m => m.County, o => o.MapFrom(s => s.County))
            .ForMember(m => m.Country, o => o.MapFrom(s => s.Country))
            .ForMember(m => m.Postcode, o => o.MapFrom(s => s.Postcode));
    }
}