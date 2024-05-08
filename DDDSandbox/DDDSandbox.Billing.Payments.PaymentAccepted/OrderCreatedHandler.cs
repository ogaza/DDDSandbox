using DDDSandbox.Billing.Messages.Commands;
using DDDSandbox.Sales.Messages.Events;
using NServiceBus;

namespace DDDSandbox.Billing.Payments.PaymentAccepted
{
  /// <summary>
  /// Handlers the OrderCreated event and all the events
  /// that inherit from it - so it also handle the 
  /// OrderCreated_V2 event
  /// </summary>
  public class OrderCreatedHandler : IHandleMessages<OrderCreated>
  {
    public async Task Handle(OrderCreated message, IMessageHandlerContext context)
    {
      Console.WriteLine($"Received order created event: OrderId: {message.OrderId}");

      var cardDetails = Database.GetCardDetailsFor(message.UserId);

      // callinig PaymentProvider can be wrapped with try catch block for example
      // and in case of failing payment we can publish let's say PaymemtRejected 
      // message consumed by the Sales bounded context. The Sales BC can then
      // for example send an email to the customer informing him of his order 
      // having been canceled
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
