namespace Factory
{
  public class RoyalMail : IShippingCourier
  {
    public string GenerateConsignmentLabelFor(Address? address)
    {
      return "RML-XXXX-XXXX-XXXX";
    }
  }
}
