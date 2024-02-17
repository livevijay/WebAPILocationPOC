using LocationAPI.Service;
using LocationAPI.Service.Interface;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt=> opt.JsonSerializerOptions.PropertyNamingPolicy = null); // Avoid naming conversion in JSON output
builder.Services.AddTransient<IData, Data>();
var app = builder.Build();

app.UseCors(x => x
.AllowAnyMethod()
.AllowAnyHeader()
.AllowCredentials()
.SetIsOriginAllowed(org => true) // set with in origin
);

app.UseAuthorization();

app.MapControllers();

app.Run();
