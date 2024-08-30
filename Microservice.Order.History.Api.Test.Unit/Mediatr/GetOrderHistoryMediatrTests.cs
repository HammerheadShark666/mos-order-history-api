using MediatR;
using Microservice.Order.History.Api.Data.Repository.Interfaces;
using Microservice.Order.History.Api.Domain;
using Microservice.Order.History.Api.Helpers.Exceptions;
using Microservice.Order.History.Api.Helpers.Interfaces;
using Microservice.Order.History.Api.Mediatr.GetOrderHistory;
using Microservice.Order.History.Api.MediatR.GetOrderHistory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System.Reflection;

namespace Microservice.Order.History.Api.Test.Unit.Mediatr;

[TestFixture]
public class GetOrderHistoryMediatrTests
{
    private readonly Mock<IOrderHistoryRepository> orderHistoryRepositoryMock = new();
    private readonly Mock<ILogger<GetOrderHistoryQueryHandler>> loggerMock = new();
    private readonly Mock<ICustomerHttpAccessor> customerHttpAccessorMock = new();
    private readonly ServiceCollection services = new();
    private ServiceProvider serviceProvider;
    private IMediator mediator;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetOrderHistoryQueryHandler).Assembly));
        services.AddScoped(sp => orderHistoryRepositoryMock.Object);
        services.AddScoped(sp => loggerMock.Object);
        services.AddScoped(sp => customerHttpAccessorMock.Object);
        services.AddAutoMapper(Assembly.GetAssembly(typeof(GetOrderHistoryMapper)));

        serviceProvider = services.BuildServiceProvider();
        mediator = serviceProvider.GetRequiredService<IMediator>();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        services.Clear();
        serviceProvider.Dispose();
    }

    [Test]
    public async Task Get_order_history_successfully_return_order()
    {
        Guid orderId = new("07c06c3f-0897-44b6-ae05-a70540e73a12");
        Guid customerId = new("29a75938-ce2d-473b-b7fe-2903fe97fd6e");
        Guid customerAddressId = new("724cbd34-3dff-4e2a-a413-48825f1ab3b9");

        var orderItem1 = new OrderItem
        {
            OrderId = orderId,
            ProductId = new Guid("29a75938-ce2d-473b-b7fe-2903fe97fd6e"),
            Name = "Infinity Kings",
            ProductType = "Book",
            Quantity = 1,
            UnitPrice = 9.99m
        };

        var orderItem2 = new OrderItem
        {
            OrderId = orderId,
            ProductId = new Guid("6131ce7e-fb11-4608-a3d3-f01caee2c465"),
            Name = "Infinity Reaper",
            ProductType = "Book",
            Quantity = 1,
            UnitPrice = 8.99m
        };

        var orderItems = new List<OrderItem>() { orderItem1, orderItem2 };

        var order = new OrderHistory
        {
            Id = orderId,
            CustomerId = customerId,
            AddressSurname = "Test",
            AddressForename = "Jake",
            AddressLine1 = "AddressLine1",
            AddressLine2 = "AddressLine2",
            AddressLine3 = "AddressLine3",
            TownCity = "TownCity",
            County = "County",
            Postcode = "Postcode",
            Country = "Country",
            OrderNumber = "000000001",
            OrderItems = orderItems,
            OrderStatus = "Completed",
            Total = 18.98m
        };

        customerHttpAccessorMock.Setup(x => x.CustomerId)
           .Returns(customerId);

        orderHistoryRepositoryMock
                .Setup(x => x.GetByIdAsync(orderId, customerId))
                .Returns(Task.FromResult(order));

        var getOrderHistoryRequest = new GetOrderHistoryRequest(orderId);
        var actualResult = await mediator.Send(getOrderHistoryRequest);

        Assert.Multiple(() =>
        {
            Assert.That(actualResult.OrderHistory.Id, Is.EqualTo(orderId));
            Assert.That(actualResult.OrderHistory.OrderNumber, Is.EqualTo("000000001"));
            Assert.That(actualResult.OrderHistory.OrderItems, Has.Count.EqualTo(2));
        });
    }

    [Test]
    public void Get_order_history_not_found_return_exception()
    {
        Guid orderId = new("07c06c3f-0897-44b6-ae05-a70540e73a12");
        Guid customerId = new("29a75938-ce2d-473b-b7fe-2903fe97fd6e");

        orderHistoryRepositoryMock
                .Setup(x => x.GetByIdAsync(orderId, customerId));

        var getOrderRequest = new GetOrderHistoryRequest(orderId);

        var validationException = Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await mediator.Send(getOrderRequest);
        });

        Assert.That(validationException.Message, Is.EqualTo($"Order history not found."));
    }
}