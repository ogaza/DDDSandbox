namespace Strategy
{
  public interface IBasketDiscountStrategy
  {
    decimal GetTotalCostAfterApplyingDiscountTo(Basket basket);
  }

  public class BasketDiscountMoneyOff : IBasketDiscountStrategy 
  {
    public decimal GetTotalCostAfterApplyingDiscountTo(Basket basket)
    {
      if (basket.TotalCost > 100) 
      {
        return basket.TotalCost - 10m;

      }
      if (basket.TotalCost > 50)
      {
        return basket.TotalCost - 5m;
      }

      return basket.TotalCost;
    }
  }

  public class BasketDiscountPercentageOff : IBasketDiscountStrategy
  {
    public decimal GetTotalCostAfterApplyingDiscountTo(Basket basket)
    {
      return basket.TotalCost * 0.85m;
    }
  }

  /// <summary>
  /// A special discount strategy to be used if no discounts are set.
  /// This is also an example of the Null Object pattern implementation.  
  /// </summary>
  public class NoBasketDiscount : IBasketDiscountStrategy
  {
    public decimal GetTotalCostAfterApplyingDiscountTo(Basket basket)
    {
      return basket.TotalCost;
    }
  }
}
