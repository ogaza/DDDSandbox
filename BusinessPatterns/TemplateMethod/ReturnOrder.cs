namespace TemplateMethod
{
  /// <summary>
  /// Represents the customer's order being return
  /// </summary>
  public class ReturnOrder 
  {
    public ReturnAction Action;
    public string? PaymentTransactionId { get; set; }
    public decimal PricePaid { get; set; }
    public decimal PostageCost { get; set; }
    public long ProductId { get; set; }
    public decimal AmountToRefund { get; set; }
  }
}
