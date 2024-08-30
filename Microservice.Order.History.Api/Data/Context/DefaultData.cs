using Microservice.Order.History.Api.Domain;

namespace Microservice.Order.History.Api.Data.Context;

public class DefaultData
{
    public static List<Domain.OrderHistory> GetOrderHistoryDefaultData()
    {
        return
        [
            CreateOrderHistory(new Guid("24331f31-a2cd-4ff4-8db6-c93d124e4483"), new Guid("6c84d0a3-0c0c-435f-9ae0-4de09247ee15"), "Test_Surname", "Test_Forename", "AddressLine1", "AddressLine2", "AddressLine3", "Leeds", "West Yorkshire", "England", "HD6 TRF4", "000000001", [], "Completed", 19.98m),
        ];
    }

    public static List<OrderItem> GetOrderItemDefaultData()
    {
        return
        [
            CreateOrderItem(new Guid("24331f31-a2cd-4ff4-8db6-c93d124e4483"), new Guid("29a75938-ce2d-473b-b7fe-2903fe97fd6e"), "Infinity Kings", "Book", 1, 9.99m),
            CreateOrderItem(new Guid("24331f31-a2cd-4ff4-8db6-c93d124e4483"), new Guid("37544155-da95-49e8-b7fe-3c937eb1de98"), "Wild Love", "Book", 1, 9.99m),
        ];
    }

    private static Domain.OrderHistory CreateOrderHistory(Guid id, Guid customerId, string addressSurname, string addressForename, string addressLine1, string addressLine2,
            string addressLine3, string townCity, string county, string country, string postcode, string orderNumber, List<OrderItem> orderItems, string orderStatus, decimal total)
    {
        return new Domain.OrderHistory
        {
            Id = id,
            CustomerId = customerId,
            AddressSurname = addressSurname,
            AddressForename = addressForename,
            AddressLine1 = addressLine1,
            AddressLine2 = addressLine2,
            AddressLine3 = addressLine3,
            TownCity = townCity,
            County = county,
            Country = country,
            Postcode = postcode,
            OrderNumber = orderNumber,
            OrderItems = orderItems,
            OrderStatus = orderStatus,
            OrderPlaced = DateOnly.FromDateTime(DateTime.Now),
            Created = DateTime.Now,
            Total = total
        };
    }

    private static OrderItem CreateOrderItem(Guid orderId, Guid productId, string name, string productType, int quantity, decimal unitPrice)
    {
        return new OrderItem
        {
            OrderId = orderId,
            ProductId = productId,
            Name = name,
            ProductType = productType,
            Quantity = quantity,
            UnitPrice = unitPrice
        };
    }
}