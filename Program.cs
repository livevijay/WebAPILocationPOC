using LocationAPI.Service;
using LocationAPI.Service.Interface;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt=> opt.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddTransient<IData, Data>();
var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseCors(x => x
.AllowAnyMethod()
.AllowAnyHeader()
.AllowCredentials()
.SetIsOriginAllowed(org => true)
);

app.UseAuthorization();

app.MapControllers();

app.Run();
