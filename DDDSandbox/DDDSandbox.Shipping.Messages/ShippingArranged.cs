namespace DDDSandbox.Shipping.Messages
{
  public class ShippingArranged : IEvent
  {
    public string? OrderId { get; set; }
  }
}
