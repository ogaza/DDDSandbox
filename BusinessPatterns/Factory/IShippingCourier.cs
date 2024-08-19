namespace Factory
{
  public interface IShippingCourier
  {
    /// <summary>
    /// Takes an address and returns a consignement id
    /// </summary>
    /// <param name="address"></param>
    /// <returns>Id as a string</returns>
    string GenerateConsignmentLabelFor(Address? address);
  }


}
