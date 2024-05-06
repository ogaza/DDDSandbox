namespace DDDSandbox.Shipping.BusinessCustomers.ShippingArranged
{
  public static class ShippingDatabase
  {
    private static List<ShippingOrder> _orders = new List<ShippingOrder>();

    public static void AddOrderDetails(ShippingOrder order)
    {
      _orders.Add(order);
    }

    public static string GetCustomerAddress(string? orderId)
    {
      return "Krakow Zablocie 43";
    }
  }

  public static class ShippingProvider
  {
    public static ShippingConfirmation ArrangeShippingFor(string address, string referenceCode) 
    { 
      return new ShippingConfirmation() 
      {
        Status = ShippingStatus.Success
      }; 
    }
  }

  public class ShippingOrder
  {
    public string? UserId { get; set; }
    public string? OrderId { get; set; }
    public string? ShippingTypeId { get; set; }
    public string? AddressId { get; set; }
  }

  public class ShippingConfirmation 
  {
    public ShippingStatus Status { get; set; }
  }

  public enum ShippingStatus
  {
    Failure,
    Success
  }
}
