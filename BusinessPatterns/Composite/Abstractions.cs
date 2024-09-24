namespace Composite
{
  public abstract class CompositeSpecification<T> : ISpecification<T>
  {
    public abstract bool IsSatisfiedBy(T candidate);

    public ISpecification<T> And(ISpecification<T> other)
    {
      return new AndSpecification<T>(this, other);
    }
    public ISpecification<T> Not()
    {
      return new NotSpecification<T>(this);
    }
  }

  public class AndSpecification<T>(
    ISpecification<T> leftSpecification,
    ISpecification<T> rightSpecification) : CompositeSpecification<T>
  {
    public override bool IsSatisfiedBy(T candidate)
    {
      return (_leftSpecification?.IsSatisfiedBy(candidate) ?? true) &&
             (_rightSpecification?.IsSatisfiedBy(candidate) ?? true);
    }

    private readonly ISpecification<T>? _leftSpecification = leftSpecification;
    private readonly ISpecification<T>? _rightSpecification = rightSpecification;
  }

  public class NotSpecification<T>(
  ISpecification<T> innerSpecification) : CompositeSpecification<T>
  {
    public override bool IsSatisfiedBy(T candidate)
    {
      return !(_innerSpecification?.IsSatisfiedBy(candidate) ?? true);
    }

    private readonly ISpecification<T>? _innerSpecification = innerSpecification;
  }

  public interface ISpecification<T>
  {
    bool IsSatisfiedBy(T candidate);

    ISpecification<T> And(ISpecification<T> other);

    ISpecification<T> Not();
  }
}
