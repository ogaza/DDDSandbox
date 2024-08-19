namespace Factory
{
  public class OrderService 
  {
    public void Dispatch(Order order) 
    {
      var shippingCourier = 
        UKShippingCourierFactory.CreateShippingCourier(order);

      order.CourierTrackingId = 
        shippingCourier?.GenerateConsignmentLabelFor(order.Address);
    }
  }
}
