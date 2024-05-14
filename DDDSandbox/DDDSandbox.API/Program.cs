using DDDSandbox.Sales.Messages.Commands;
using NServiceBus;

var builder = WebApplication.CreateBuilder(args);

var webClientCorsPolicyName = "DDDSandbox.Web";
ConfigureCors(builder, webClientCorsPolicyName);
ConfigureBus(builder);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(webClientCorsPolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureCors(WebApplicationBuilder builder, string webClientCorsPolicyName)
{
  builder.Services.AddCors(options =>
  {
    options.AddPolicy(
        name: webClientCorsPolicyName,
        policy =>
        {
          policy
            //.AllowAnyOrigin();
            .WithOrigins("http://localhost:5292").AllowAnyMethod().AllowAnyHeader();
        });
  });
}

static void ConfigureBus(WebApplicationBuilder builder)
{
  builder.Host.UseNServiceBus(context =>
    {
      var endpointConfiguration =
      new EndpointConfiguration("DDDSandbox.API");

      var transport = endpointConfiguration.UseTransport(new LearningTransport());

      transport.RouteToEndpoint(
          assembly: typeof(PlaceOrder).Assembly,
          destination: "DDDSandbox.Sales.Orders.OrderCreated");
      endpointConfiguration.UseSerialization<SystemJsonSerializer>();
      //endpointConfiguration.SendOnly();

      //endpointConfiguration.MakeInstanceUniquelyAddressable("1");
      //endpointConfiguration.EnableCallbacks();

      //var endpointInstance = await Endpoint.Start(endpointConfiguration);

      return endpointConfiguration;
    });
}

