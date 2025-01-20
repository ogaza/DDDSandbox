namespace LayerSupertype
{
  public class Customer : EntityBase<long>
  {
    public Customer(long id) : base(id) { }

    public string? Name { get; set; }

    protected override void CheckForBrokenRules()
    {
      if (string.IsNullOrEmpty(Name)) 
      {
        AddBrokenRule("Neme is not specified");
      }
    }
  }

  public abstract class EntityBase<T>
  {
    private T _id;

    private IList<string> _brokenRules = new List<string>();

    private bool _idHasBeenSet = false;

    public EntityBase() { }

    public EntityBase(T id)
    {
      _id = id;
    }

    public T Id 
    {
      get { return _id; }

      set
      {
        if (_idHasBeenSet) { throw new ApplicationException(); }

        _id = value;

        _idHasBeenSet = true;
      }
    }

    public bool IsValid() 
    {
      ClearCollectionOfBrokenRules();
      CheckForBrokenRules();

      return _brokenRules.Count() == 0;
    }

    protected abstract void CheckForBrokenRules();

    private void ClearCollectionOfBrokenRules()
    {
      _brokenRules.Clear();
    }

    protected void AddBrokenRule(string brokenRule)
    {
      _brokenRules.Add(brokenRule);
    }
  }
}
