namespace Composite
{
  public class CustomerAccount
  {

    public bool CanRent() 
    {
      var canRentSpecification =
            _customerAccountIsActive.And(_hasReachedRentalThreshold.Not())
                                    .And(_customerAccountHasLateFees.Not());

      return canRentSpecification.IsSatisfiedBy(this);
    }
    public int NumberOfRentals { get; set; }

    public bool AccountIsActive { get; set; }

    public decimal LateFees { get; set; }

    public CustomerAccount()
    {
      _hasReachedRentalThreshold = new HasReachedRentalThreshold();
      _customerAccountIsActive = new CustomerAccountIsActive();
      _customerAccountHasLateFees = new CustomerAccountHasLateFees();
    }

    private readonly HasReachedRentalThreshold _hasReachedRentalThreshold;
    private readonly CustomerAccountIsActive _customerAccountIsActive;
    private readonly CustomerAccountHasLateFees _customerAccountHasLateFees;

  }
}
