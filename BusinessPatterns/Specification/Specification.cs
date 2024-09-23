namespace Specification
{
  public class CustomerAccount
  {

    public bool CanRent() => _hasReachedRentalThreshold.IsSatisfiedBy(this);
    public int NumberOfRentals { get; set; }

    public CustomerAccount() 
    {
      _hasReachedRentalThreshold = new HasReachedRentalThreshold();
    }

    private readonly HasReachedRentalThreshold _hasReachedRentalThreshold;
    
  }

  public class HasReachedRentalThreshold : ISpecification<CustomerAccount>
  {
    public bool IsSatisfiedBy(CustomerAccount candidate) => candidate.NumberOfRentals > 5;
  }

  /// <summary>
  /// Specification encapsulates business logic in a boolean
  /// algorithm. Separates selection criteria for given 
  /// entities from the entity itself so the criteria
  /// can be shared or reused.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public interface ISpecification<T>
  {
    bool IsSatisfiedBy(T candidate);
  }
}
