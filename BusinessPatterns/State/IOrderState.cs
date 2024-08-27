namespace State
{
  public interface IOrderState
  {
    OrderStatus Status { get; }
    bool CanShip(Order order);
    void Ship(Order order);
    bool CanCancel(Order order);
    void Cancel(Order order);
  }
}
