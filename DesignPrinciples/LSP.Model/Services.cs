namespace LSP.Model
{
  public abstract class PaymentServiceBase 
  {
    public abstract string Refund(decimal amount, string transactionId);
  }

  public class PayPalPayment : PaymentServiceBase
  {
    public override string Refund(decimal amount, string transactionId)
    {
      var paymentService = new MockPayPalWebService();
      var token = paymentService.ObtainToken(AccountName, Password);
      var response = paymentService.MakeRefund(amount, transactionId, token);

      return response;
    }

    public string AccountName { get; set; }
    public string Password { get; set; }
  }

  public class WorldPayPayment : PaymentServiceBase
  {
    public override string Refund(decimal amount, string transactionId)
    {
      var paymentService = new MockWorldPayWebService();
      var response = 
        paymentService
          .MakeRefund(
            amount, 
            transactionId, 
            AccountId, 
            AccountPassword, 
            ProductId);

      return response;
    }

    public string AccountId { get; set; }
    public string AccountPassword { get; set; }
    public string ProductId { get; set; }
  }

  public enum PaymentType 
  {
    PayPal = 1,
    WorldPay = 2
  }

  public class MockPayPalWebService 
  {
    public string ObtainToken(string accountName, string password)
    {
      return "XXXX-XX-XXXX";
    }

    public string MakeRefund(decimal amount, string transactionId, string token)
    {
      return "Auth:0999";
    }
  }

  public class MockWorldPayWebService 
  {
    public string MakeRefund(
      decimal amount,
      string transactionId,
      string username,
      string password,
      string productId)
    {
      return "A_success:09901";
    }
  }
}
