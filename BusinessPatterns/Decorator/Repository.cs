
namespace Decorator
{
  public interface IProductRepository
  {
    IEnumerable<Product> FindAll();
  }

  public class Repository
  {

  }
}
