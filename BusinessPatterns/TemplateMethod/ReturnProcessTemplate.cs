namespace TemplateMethod
{
  public abstract class ReturnProcessTemplate 
  {
    public void Process(ReturnOrder returnOrder) 
    {
      GenerateReturnTransactionFor(returnOrder);
      CalculateRefundFor(returnOrder);
    }

    protected abstract void GenerateReturnTransactionFor(ReturnOrder returnOrder);
    protected abstract void CalculateRefundFor(ReturnOrder returnOrder);
  }

  public class FaultyReturnProcess : ReturnProcessTemplate
  {
    protected override void CalculateRefundFor(ReturnOrder returnOrder)
    {
      returnOrder.AmountToRefund =
          returnOrder.PricePaid + returnOrder.PostageCost;
    }

    protected override void GenerateReturnTransactionFor(ReturnOrder returnOrder)
    {
      // code to send item back to manufacturer
    }
  }

  public class NoQuibblesReturnProcess : ReturnProcessTemplate
  {
    protected override void CalculateRefundFor(ReturnOrder returnOrder)
    {
      returnOrder.AmountToRefund = returnOrder.PricePaid;
    }

    protected override void GenerateReturnTransactionFor(ReturnOrder returnOrder)
    {
      // code to put items back into stock
    }
  }

  public class ReturnService
  {
    public void Process(ReturnOrder returnOrder) 
    {
      ReturnProcessTemplate returnProcess =
        ReturnProcessFactory.CreateFrom(returnOrder.Action);

      returnProcess.Process(returnOrder);

      // code to refund money back to the customer
    }
  }
}
