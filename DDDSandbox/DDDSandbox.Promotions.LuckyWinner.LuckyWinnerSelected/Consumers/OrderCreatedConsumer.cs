using DDDSandbox.Promotions.LuckyWinner.LuckyWinnerSelected.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace DDDSandbox.Promotions.LuckyWinner.LuckyWinnerSelected.Consumers
{
  public class OrderCreatedConsumer : IConsumer<OrderCreated>
  {
    public Task Consume(ConsumeContext<OrderCreated> context)
    {
      _logger.LogInformation($"Mass Transit Order Created Consumer. OrderId: {context.Message.OrderId}");

      return Task.CompletedTask;
    }


    readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
    {
      _logger = logger;
    }
  }
}
