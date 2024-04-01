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
          policy.WithOrigins("http://localhost:5292");
        });
  });
}

static void ConfigureBus(WebApplicationBuilder builder)
{
  builder.Host.UseNServiceBus(context =>
    {
      var endpointConfiguration =
      new EndpointConfiguration("DDDSandbox.API");
      endpointConfiguration.MakeInstanceUniquelyAddressable("1");
      //endpointConfiguration.EnableCallbacks();
      endpointConfiguration.UseTransport(new LearningTransport());

      return endpointConfiguration;
    });
}

