namespace Composite
{
  public class HasReachedRentalThreshold : CompositeSpecification<CustomerAccount>
  {
    public override bool IsSatisfiedBy(CustomerAccount candidate) => candidate.NumberOfRentals > 5;
  }

  public class CustomerAccountIsActive : CompositeSpecification<CustomerAccount>
  {
    public override bool IsSatisfiedBy(CustomerAccount candidate) => candidate.AccountIsActive;
  }

  public class CustomerAccountHasLateFees : CompositeSpecification<CustomerAccount>
  {
    public override bool IsSatisfiedBy(CustomerAccount candidate) => candidate.LateFees > 0;
  }
}
