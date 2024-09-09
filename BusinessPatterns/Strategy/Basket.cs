namespace Strategy
{
  public class Basket
  {
    private readonly IBasketDiscountStrategy _basketDiscount;

    public Basket(DiscountType discountType)
    {
      _basketDiscount = BasketDiscountFactory.GetDiscount(discountType);
    }

    public decimal GetTotalCostAfterDiscount() => _basketDiscount.GetTotalCostAfterApplyingDiscountTo(this);

    public decimal TotalCost { get; set; }
  }
}
