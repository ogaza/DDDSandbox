namespace Factory
{
  public class DHL : IShippingCourier
  {
    public string GenerateConsignmentLabelFor(Address? address)
    {
      return "DHL-XXXX-XXXX-XXXX";
    }
  }
}
