namespace State
{
  public class Order(IOrderState state)
  {
    private IOrderState _state = state;

    public int Id { get; set; }

    public string Customer {  get; set; }

    public DateTime OrderDate { get; set; }

    public OrderStatus Status() => _state.Status;
    public bool CanCancel() => _state.CanCancel(this);
    public void Cancel()
    {
      if (!_state.CanCancel(this))
      {
        return;
      }
      _state.Cancel(this);
    }
    public bool CanShip() => _state.CanShip(this);
    public void Ship()
    {
      if (!_state.CanShip(this))
      {
        return;
      }
      _state.Ship(this);
    }
    internal void Change(IOrderState orderState) 
    {
      _state = orderState;
    }
  }
}
