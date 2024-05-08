using DDDSandbox.Sales.Messages.Commands;
using DDDSandbox.Sales.Messages.Events;
using NServiceBus;

namespace DDDSandbox.Sales.Orders.OrderCreated.Handlers
{
  public class PlaceOrderHandler :
    IHandleMessages<PlaceOrder>
  {
    //static ILog log = LogManager.GetLogger<MyHandler>();

    public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
    {
      var id = DataBase.SaveOrder(message);

      // this handler sends new version of the order created event
      // since OrderCreated_V2 inherits from OrderCreated
      // all the OrderCreated handlers will also handle the 
      // OrderCreated_V2 event
      var orderCreatedEvent = new OrderCreated_V2 { OrderId = $"{id}", AddressId = "SomeAddressId" };
      await context.Publish(orderCreatedEvent);
      
      
      //await context.Reply(message);
      //log.Info("Message received at endpoint");
      //return Task.CompletedTask;
    }
  }

  public static class DataBase
  {
    private static int Id = 0;

    private static readonly List<PlaceOrder> orderNames = new();

    internal static int SaveOrder(PlaceOrder order)
    {
      if (order == null) 
      {
        return 0;
      }
      orderNames.Add(order);

      return ++Id;
    }
  }
}
