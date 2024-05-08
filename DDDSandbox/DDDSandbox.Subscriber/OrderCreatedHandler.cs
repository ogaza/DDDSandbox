using DDDSandbox.Sales.Messages.Events;
using NServiceBus;

namespace DDDSandbox.Subscriber
{
  public class OrderCreatedHandler : IHandleMessages<OrderCreated>
  {
    public Task Handle(OrderCreated message, IMessageHandlerContext context)
    {
      return Task.CompletedTask;
    }
  }
}
