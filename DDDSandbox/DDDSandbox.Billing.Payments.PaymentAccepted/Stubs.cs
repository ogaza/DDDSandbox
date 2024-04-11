using DDDSandbox.Billing.Messages.Commands;

namespace DDDSandbox.Billing.Payments.PaymentAccepted
{
  public static class PaymentProvider
  {
    private static int Attempts = 0;

    public static PaymentConfirmation ChargeCreditCard(CardDetails details, double? amount)
    {
      if (Attempts < 2)
      {
        Attempts++;

        throw new Exception("Service unavailable.");
      }

      Attempts = 0;

      return new PaymentConfirmation { Status = PaymentStatus.Accepted };
    }
  }

  public class PaymentConfirmation
  {
    public PaymentStatus Status { get; set; }
  }

  public static class Database
  {
    public static CardDetails GetCardDetailsFor(string userId)
    {
      return new CardDetails();
    }

    public static void SavePaymentAttempt(string? orderId, PaymentStatus status)
    {
      // throw new NotImplementedException();
    }
  }

  public class CardDetails { }
}
