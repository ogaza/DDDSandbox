using Model;

namespace DependencyInjection
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var discount = new ChristmasProductDiscount();
      var repository = new LiqProductRepository();

      var service = new ProductService(repository);

      var products = service.GetProductsAndApplyDiscounts(discount);
      foreach (var item in products)
      {
        Console.WriteLine(item);
      }
    }
  }
}
