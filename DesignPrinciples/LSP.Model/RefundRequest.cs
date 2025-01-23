namespace LSP.Model
{
  public class RefundRequest
  {
    public PaymentType PaymentType { get; set; }
    public string PaymentTransactionId { get; set; }
    public decimal RefundAmount { get; set; }
  }
}
