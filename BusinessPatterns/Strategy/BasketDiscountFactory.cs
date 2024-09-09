namespace Strategy
{
  public class BasketDiscountFactory
  {
    public static IBasketDiscountStrategy GetDiscount(DiscountType DiscountType)
    {
      switch (DiscountType)
      {
        case DiscountType.MoneyOff:
          return new BasketDiscountMoneyOff();
        case DiscountType.PercentageOff:
          return new BasketDiscountPercentageOff();
        default:
          return new NoBasketDiscount();
      }
    }
  }

  public enum DiscountType
  {
    NoDiscount = 0,
    MoneyOff = 1,
    PercentageOff = 2
  }
}
