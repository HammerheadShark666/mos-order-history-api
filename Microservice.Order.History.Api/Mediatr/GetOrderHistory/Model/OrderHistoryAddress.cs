﻿namespace Microservice.Order.History.Api.Mediatr.GetOrderHistory.Model;

public class OrderHistoryAddress
{
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string AddressLine3 { get; set; }
    public string TownCity { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
    public string Country { get; set; }
}