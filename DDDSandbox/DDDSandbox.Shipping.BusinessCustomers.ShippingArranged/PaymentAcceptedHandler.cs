using DDDSandbox.Billing.Messages.Events;
using NServiceBus;

namespace DDDSandbox.Shipping.BusinessCustomers.ShippingArranged
{
  public class PaymentAcceptedHandler : IHandleMessages<PaymentAccepted>
  {
    public async Task Handle(PaymentAccepted message, IMessageHandlerContext context)
    {
      Console.WriteLine($"Received {message.GetType().Name} event: OrderId: {message.OrderId}");

      var address = ShippingDatabase.GetCustomerAddress(message.OrderId);

      var confirmation = ShippingProvider.ArrangeShippingFor(address, message.OrderId);

      if(confirmation.Status == ShippingStatus.Success) 
      {
        Console.WriteLine($"Shipping arranged for order {message.OrderId} to: '{address}' ");

        var evnt = new Messages.ShippingArranged { OrderId = message.OrderId };
        await context.Publish(evnt);

        return;
      }

      throw
        new Exception($"Shipping arrangement for order {message.OrderId} to: '{address}' FAILED");
    }
  }
}
