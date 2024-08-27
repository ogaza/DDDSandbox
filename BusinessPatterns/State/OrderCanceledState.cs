namespace State
{
  public class OrderCanceledState : IOrderState
  {
    public OrderStatus Status 
    {
      get => OrderStatus.Canceled;
    }

    public bool CanCancel(Order order) => false;

    public void Cancel(Order order) => throw new NotImplementedException();

    public bool CanShip(Order order) => false;

    public void Ship(Order order) => throw new NotImplementedException();
  }
}
