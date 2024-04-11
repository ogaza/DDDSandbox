namespace DDDSandbox.Billing.Messages.Commands
{
  public class PlaceOrder
  {
    public Guid UserId { get; set; }
  }  
  
  public class RecordPaymentAttempt
  {
    public string? OrderId { get; set; }

    public PaymentStatus Status { get; set; }
  }

  public class RecordCompletedPayment
  {
    public string? OrderId { get; set; }

    public PaymentStatus Status { get; set; }
  }

  public enum PaymentStatus { Accepted, Rejected }
}