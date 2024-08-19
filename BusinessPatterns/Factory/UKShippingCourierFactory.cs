namespace Factory
{
  public static class UKShippingCourierFactory
  {
    public static IShippingCourier? CreateShippingCourier(Order? order)
    {
      if (order == null)
        return null;

      if ((order.TotalCost > 100) || (order.WeightInKg > 5)) 
      {
        return new DHL();
      }
      return new RoyalMail();
    }
  }
}
