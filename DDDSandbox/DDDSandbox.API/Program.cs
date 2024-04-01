var builder = WebApplication.CreateBuilder(args);

var webClientCorsPolicyName = "DDDSandbox.Web";
builder.Services.AddCors(options =>
{
  options.AddPolicy(
      name: webClientCorsPolicyName,
      policy =>
      {
        policy.WithOrigins("http://localhost:5292");
      });
});

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

app.UseStaticFiles();

app.UseCors(webClientCorsPolicyName);

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
