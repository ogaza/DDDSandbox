using DDDSandbox.Sales.Messages.Events;
using NServiceBus;

namespace DDDSandbox.Promotions.LuckyWinner.Bridge.Handlers
{
  public class OrderCreatedHandler : IHandleMessages<OrderCreated>
  {
    public Task Handle(OrderCreated message, IMessageHandlerContext context)
    {
      Console.WriteLine($"Received order created event: OrderId: {message.OrderId}");
  
      return Task.CompletedTask;
    }
  }
}
