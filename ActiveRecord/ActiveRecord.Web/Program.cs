using ActiveRecord.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

string blogDBConnectionString = 
  builder.Configuration.GetConnectionString("BlogDB");
DBConfiguration.SetConnectionString(blogDBConnectionString);

app.MapControllers();
app.UseStaticFiles();
app.Run();