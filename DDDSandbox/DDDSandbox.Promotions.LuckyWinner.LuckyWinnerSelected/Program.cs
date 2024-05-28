// See https://aka.ms/new-console-template for more information

//Console.Title = "DDDSandbox.Promotions.LuckyWinner.LuckyWinnerSelected";

using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace DDDSandbox.Promotions.LuckyWinner.LuckyWinnerSelected 
{

  public class Program
  {
    public static async Task Main(string[] args)
    {
      await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args).ConfigureServices(services => 
      {
        services.AddMassTransit(x => 
        {
          x.SetKebabCaseEndpointNameFormatter();

          x.SetInMemorySagaRepositoryProvider();

          var entryAssembly = Assembly.GetEntryAssembly();

          x.AddConsumers(entryAssembly);
          x.AddSagaStateMachines(entryAssembly);
          x.AddSagas(entryAssembly);
          x.AddActivities(entryAssembly);
          x.UsingInMemory((ctx, cfg) =>
          {
            cfg.ConfigureEndpoints(ctx);
          });
        });

        //services.AddHostedService<Worker>();
      });
    }
  }

}
