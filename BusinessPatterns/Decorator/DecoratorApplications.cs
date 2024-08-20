
namespace Decorator
{
  /// <summary>
  /// Contains two methods which simply iterate through 
  /// the collection of products and apply a decorator 
  /// to each product's price.
  /// </summary>
  public static class ProductCollectionExtensionMethods
  {
    /// <summary>
    /// Here the ecxhange rate is hard-coded. Usually we
    /// use for example a factory to get such a value.
    /// </summary>
    /// <param name="products"></param>
    public static void ApplyCurrencyMultiplier(
      this IEnumerable<Product> products)
    {
      foreach (Product p in products)
        p.Price = new CurrencyPriceDecorator(p.Price, 0.78m);
    }

    public static void ApplyTradeDiscount(
      this IEnumerable<Product> products)
    {
      foreach (Product p in products)
        p.Price = new TradeDiscountPriceDecorator(p.Price);
    }
  }
}
