namespace DDDSandbox.Sales.Messages.Commands
{
  public class PlaceOrder : ICommand
  {
    public Guid? UserId { get; set; }
    public string[]? ProductIds { get; set; }
    public string? ShippingTypeId { get; set; }
    public DateTime? TimeStamp { get; set; }
  }
}