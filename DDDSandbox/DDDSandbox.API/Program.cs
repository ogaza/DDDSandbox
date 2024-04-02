using DDDSandbox.Messages;
using NServiceBus;

var builder = WebApplication.CreateBuilder(args);

/*
var endpointConfiguration = new EndpointConfiguration("Samples.ASPNETCore.Sender");
var transport = endpointConfiguration.UseTransport(new LearningTransport());
transport.RouteToEndpoint(
    assembly: typeof(MyMessage).Assembly,
    destination: "DDFSandbox.Endpoint");

endpointConfiguration.UseSerialization<SystemJsonSerializer>();
endpointConfiguration.SendOnly();

builder.UseNServiceBus(endpointConfiguration);
*/

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
          policy.WithOrigins("http://localhost:5292");
        });
  });
}

static void ConfigureBus(WebApplicationBuilder builder)
{
  builder.Host.UseNServiceBus(context =>
    {
     
      
      //builder.UseNServiceBus(endpointConfiguration);

      var endpointConfiguration =
      new EndpointConfiguration("DDDSandbox.API");

      var transport = endpointConfiguration.UseTransport(new LearningTransport());
      transport.RouteToEndpoint(
          assembly: typeof(MyMessage).Assembly,
          destination: "DDDSandbox.Endpoint");
      endpointConfiguration.UseSerialization<SystemJsonSerializer>();
      //endpointConfiguration.SendOnly();

      //endpointConfiguration.MakeInstanceUniquelyAddressable("1");
      //endpointConfiguration.EnableCallbacks();
      //endpointConfiguration.UseTransport(new LearningTransport());
      //endpointConfiguration.UseSerialization<SystemJsonSerializer>();

      //var endpointInstance = await Endpoint.Start(endpointConfiguration);

      return endpointConfiguration;
    });
}

