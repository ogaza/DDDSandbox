namespace DDDSandbox.Billing.Messages.Events
{
  public class PaymentAccepted : IEvent
  {
    public string? OrderId { get; set; }
  }
}
