namespace Model
{
  public class ProductService
  {
    public IEnumerable<Product> GetProductsAndApplyDiscounts(IProductDiscountStrategy discountStrategy)
    {
      var products = _repository.FindAll();

      foreach (var product in products)
      {
        product.AdjustPriceWith(discountStrategy);
      }

      return products;
    }

    private IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
      _repository = repository;
    }
  }

  public interface IProductDiscountStrategy
  {
  }

  public class ChristmasProductDiscount : IProductDiscountStrategy
  {
  }

  public interface IProductRepository
  {
    IEnumerable<Product> FindAll();
  }
  public class LiqProductRepository : IProductRepository
  {
    public IEnumerable<Product> FindAll()
    {
      return
      [
        new Product()
      ];
    }
  }

  public class Product 
  {
    public void AdjustPriceWith(IProductDiscountStrategy discount) 
    {
    }
  }
}
