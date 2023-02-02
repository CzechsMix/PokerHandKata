var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();

var app = builder.Build();

app.Urls.Add("https://localhost:7020");
app.MapControllers();

app.Run();

// Time spent on app
// 2023-01-23: 45 minutes
// 2023-01-24: 60 minutes
// 2023-01-25: 75 minutes
// 2023-01-27: 45 minutes
// 2023-01-30: 60 minutes
// 2023-02-01: 60 minutes