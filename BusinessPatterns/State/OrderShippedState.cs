namespace State
{
  public class OrderShippedState : IOrderState
  {
    public OrderStatus Status
    {
      get => OrderStatus.Shipped;
    }

    public bool CanCancel(Order order) => false;

    public void Cancel(Order order) => throw new NotImplementedException();

    public bool CanShip(Order order) => false;

    public void Ship(Order order) => throw new NotImplementedException();
  }
}
