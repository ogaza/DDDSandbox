using DDDSandbox.Messages.Events;
using NServiceBus;

namespace DDDSandbox.Shipping.BusinessCustomers.ShippingArranged
{
  public class OrderCreatedHandler : IHandleMessages<OrderCreated_V2>
  {
    public Task Handle(OrderCreated_V2 message, IMessageHandlerContext context)
    {
      Console.WriteLine($"Received {message.GetType().Name} event: OrderId: {message.OrderId}");

      var order = new ShippingOrder 
      { 
        OrderId = message.OrderId, 
        UserId = message.UserId, 
        ShippingTypeId = message.ShippingTypeId,
        AddressId = message.AddressId
      };

      ShippingDatabase.AddOrderDetails( order );

      return Task.CompletedTask;
    }
  }
}
