namespace LSP.Model
{
  public class RefundService
  {
    public RefundResponse Refund(RefundRequest request)
    {
      var paymentService = PaymentServiceFactory.CreatePaymentServiceFrom(request.PaymentType);
      var refundResponse = new RefundResponse();

      var payPalService = paymentService as PayPalPayment;
      var worldPayService = paymentService as WorldPayPayment;

      if (payPalService != null) 
      {
        payPalService.AccountName = "PP-12";
        payPalService.Password = "passPP";
      }
      if (worldPayService as WorldPayPayment != null)
      {
        worldPayService.AccountId = "123";
        worldPayService.AccountPassword = "123";
        worldPayService.ProductId = "pr-01";
      }

      string merchantResponse = 
        paymentService.Refund(request.RefundAmount, request.PaymentTransactionId);

      if (merchantResponse.Contains("Auth") || merchantResponse.Contains("A_success"))
      {
        refundResponse.Success = true;
      }
      else 
      {
        refundResponse.Success = false;
      }

      return refundResponse;
    }
  }
}
