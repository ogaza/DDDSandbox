namespace ISP.Model
{
  public interface IProduct
  {
    decimal Price { get; set; }
    decimal WeightInKg { get; set; }
    int Stock {  get; set; }
  }

  public interface IMovie
  {
    int Certification { get; set; }
    int RunningTime { get; set; }
  }

  public class DVD : IProduct, IMovie
  {
    public decimal Price { get; set;}
    public decimal WeightInKg { get; set; }
    public int Stock { get; set; }
    public int Certification { get; set; }
    public int RunningTime { get; set; }
  }

  public class BlueRay : IProduct, IMovie
  {
    public decimal Price { get; set; }
    public decimal WeightInKg { get; set; }
    public int Stock { get; set; }
    public int Certification { get; set; }
    public int RunningTime { get; set; }
  }
  
  public class TShirt : IProduct
  {
    public decimal Price { get; set; }
    public decimal WeightInKg { get; set; }
    public int Stock { get; set; }
  }
}
