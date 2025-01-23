namespace LSP.Model
{
  public class PaymentServiceFactory
  {
    public static PaymentServiceBase CreatePaymentServiceFrom(PaymentType paymentType) 
    {
      switch (paymentType) 
      {
        case PaymentType.PayPal:
          return new PayPalPayment();

        case PaymentType.WorldPay:
          return new WorldPayPayment();

        default:
          throw new ApplicationException(
            $"no payment service available for {paymentType}");
      }
    }
  }
}
