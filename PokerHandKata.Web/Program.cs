var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();

var app = builder.Build();

app.Urls.Add("https://localhost:7020");
app.MapControllers();

app.Run();