// See https://aka.ms/new-console-template for more information
using Model;

var discount = new ChristmasProductDiscount();
var repository = new LiqProductRepository();

var service = new ProductService(repository);

var products = service.GetProductsAndApplyDiscounts(discount);
foreach (var item in products)
{
  Console.WriteLine(item);
}

Console.WriteLine("press any key to close");
Console.ReadKey();