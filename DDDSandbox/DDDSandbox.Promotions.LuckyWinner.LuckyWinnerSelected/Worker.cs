using DDDSandbox.Promotions.LuckyWinner.LuckyWinnerSelected.Events;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace DDDSandbox.Promotions.LuckyWinner.LuckyWinnerSelected
{
  public class Worker : BackgroundService
  {
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested) 
      {
        await _bus.Publish(new OrderCreated { OrderId = $"{Guid.NewGuid()}" }, stoppingToken);

        await Task.Delay(1000, stoppingToken);
      }
    }

    readonly IBus _bus;

    public Worker(IBus bus)
    {
      _bus = bus;
    }
  }
}
