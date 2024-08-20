
namespace Decorator
{
  /// <summary>
  /// Coordinates the retrieval and application 
  /// of the trade discount and currency multiplication.
  /// </summary>
  /// <param name="productRepository"></param>
  public class ProductService(IProductRepository productRepository)
  {
    private readonly IProductRepository _productRepository = productRepository;

    /// <summary>
    /// Gets a collection of products and wraps each item of it
    /// with <see cref="TradeDiscountPriceDecorator" /> and 
    /// with <see cref="CurrencyPriceDecorator" />
    /// </summary>
    /// <returns>
    /// A collection of products decorated with the trade 
    /// discount and the currency multiplication behavior
    /// </returns>
    public IEnumerable<Product> GetAllProducts()
    {
      IEnumerable<Product> products = _productRepository.FindAll();
      products.ApplyTradeDiscount();
      products.ApplyCurrencyMultiplier();

      return products;
    }
  }
}
