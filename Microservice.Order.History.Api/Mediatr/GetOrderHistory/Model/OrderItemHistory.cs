namespace Microservice.Order.History.Api.Mediatr.GetOrderHistory.Model;

public class OrderItemHistory
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ProductType { get; set; } = string.Empty;
    public decimal? UnitPrice { get; set; }
    public int Quantity { get; set; }
}