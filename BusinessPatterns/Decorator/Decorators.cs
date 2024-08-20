
///
/// As well as adding new functionality, 
/// decorators can restrict functionality; 
/// a security decorator can ensure only 
/// users with certain privileges can call 
/// methods or routines. Decorators are 
/// also good for wrapping infrastructure 
/// code like logging
///
namespace Decorator
{
  public class Product
  {
    public IPrice? Price { get; set; }
  }

  public interface IPrice
  {
    decimal Cost { get; set; }
  }

  /// <summary>
  /// Default behavior of the product's price. 
  /// Can be set for example by the repository 
  /// when hydrating a list of products from 
  /// the data store.
  /// </summary>
  public class BasePrice : IPrice
  {
    private decimal _cost;
    public decimal Cost
    {
      get { return _cost; }
      set { _cost = value; }
    }
  }

  /// <summary>
  ///  Decorates the default price behavior with 
  ///  the logic that applies a trade discount.
  ///  Wrap an implementation of the <see cref="IPrice"/> 
  ///  supplied via the constructor, and reduce the cost 
  ///  by a factor of 5 percent.
  ///  Any client referencing an interface, 
  ///  will be unaware that they are talking 
  ///  to the <see cref="TradeDiscountPriceDecorator" />.
  /// </summary>
  public class TradeDiscountPriceDecorator(IPrice price) : IPrice
  {
    private readonly IPrice _basePrice = price;

    public decimal Cost
    {
      get { return _basePrice.Cost * 0.95m; }
      set { _basePrice.Cost = value; }
    }
  }

  /// <summary>
  ///  Decorates an implementation of <see cref="IPrice"/>
  ///  with the currency multiplication.
  /// </summary>
  /// <param name="price"></param>
  /// <param name="exchangeRate"></param>
  public class CurrencyPriceDecorator(
    IPrice price, 
    decimal exchangeRate
  ) 
    : IPrice
  {
    private readonly IPrice _basePrice = price;
    private readonly decimal _exchangeRate = exchangeRate;

    public decimal Cost
    {
      get { return _basePrice.Cost * _exchangeRate; }
      set { _basePrice.Cost = value; }
    }
  }
}
