using DDDSandbox.Messages;
using DDDSandbox.Messages.Events;
using NServiceBus;

namespace DDDSandbox.Endpoint.Handlers
{
  public class MyHandler :
    IHandleMessages<MyMessage>
  {
    //static ILog log = LogManager.GetLogger<MyHandler>();

    public async Task Handle(MyMessage message, IMessageHandlerContext context)
    {
      var id = DataBase.SaveOrder(message.Name);

      var orderCreatedEvent = new OrderCreated { OrderId = $"{id}" };
      await context.Publish(orderCreatedEvent);
      
      
      //await context.Reply(message);
      //log.Info("Message received at endpoint");
      //return Task.CompletedTask;
    }
  }

  public static class DataBase 
  {
    private static int Id = 0;

    private static readonly List<string> orderNames = new();

    internal static int SaveOrder(string? name)
    {
      if (name == null) 
      {
        return 0;
      }
      orderNames.Add(name);

      return ++Id;
    }
  }
}
