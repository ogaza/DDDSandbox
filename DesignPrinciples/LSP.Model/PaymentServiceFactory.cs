namespace LSP.Model
{
  public class PaymentServiceFactory
  {
    public static PaymentServiceBase CreatePaymentServiceFrom(PaymentType paymentType) 
    {
      switch (paymentType) 
      {
        case PaymentType.PayPal:
          // the hardcoded values just for the simplicity
          // of the example - in real app this values
          // should probably be read from the app configuration
          return new PayPalPayment("PP-12", "passPP");

        case PaymentType.WorldPay:
          return new WorldPayPayment("acc123", "pass123", "pr-01");

        default:
          throw new ApplicationException(
            $"no payment service available for {paymentType}");
      }
    }
  }
}
