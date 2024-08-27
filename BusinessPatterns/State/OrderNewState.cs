namespace State
{
  public class OrderNewState : IOrderState
  {
    public OrderStatus Status
    {
      get => OrderStatus.New;
    }

    public bool CanCancel(Order order) => true;

    public void Cancel(Order order) 
    {
      order.Change(new OrderCanceledState());
    }

    public bool CanShip(Order order) => true;

    public void Ship(Order order)
    {
      order.Change(new OrderShippedState());
    }
  }
}
