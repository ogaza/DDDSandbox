using DDDSandbox.Billing.Messages.Commands;
using DDDSandbox.Messages.Events;
using NServiceBus;

namespace DDDSandbox.Billing.Payments.PaymentAccepted
{
  public class OrderCreatedHandler : IHandleMessages<OrderCreated>
  {
    public async Task Handle(OrderCreated message, IMessageHandlerContext context)
    {
      Console.WriteLine($"Received order created event: OrderId: {message.OrderId}");

      var cardDetails = Database.GetCardDetailsFor(message.UserId); 
      
      var conf = PaymentProvider.ChargeCreditCard(cardDetails, message.Amount);
      
      var command = new RecordPaymentAttempt { OrderId = message.OrderId, Status = conf.Status };
      await context.SendLocal(command);
    }
  }

  public class RecordPaymentAttemptHandler : IHandleMessages<RecordPaymentAttempt>
  {
    public async Task Handle(RecordPaymentAttempt message, IMessageHandlerContext context)
    {
      Database.SavePaymentAttempt(message.OrderId, message.Status);

      if(message.Status == PaymentStatus.Accepted) 
      {
        await context.Publish(new Messages.Events.PaymentAccepted { OrderId = message.OrderId });
      }
      else
      {
        // policy for failed payment operation 
        // for example publishing payment rejected event
      }
    }
  }

  public class PaymentAcceptedHandler : IHandleMessages<Messages.Events.PaymentAccepted>
  {
    public Task Handle(Messages.Events.PaymentAccepted message, IMessageHandlerContext context)
    {
      return Task.CompletedTask;
    }
  }
}
