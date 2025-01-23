namespace LSP.Model
{
  public class RefundService
  {
    public RefundResponse Refund(RefundRequest request)
    {
      var paymentService = PaymentServiceFactory.CreatePaymentServiceFrom(request.PaymentType);
      var refundResponse = 
        paymentService.Refund(request.RefundAmount, request.PaymentTransactionId);

      return refundResponse;
    }
  }
}
