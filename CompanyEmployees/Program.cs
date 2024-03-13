using CompanyEmployees.Extensions;
using LoggerService;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Logging.Abstractions;
using NLog;
var builder = WebApplication.CreateBuilder(args);

//LoggerManager.Setup().LoadCongiruationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();



builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
		app.UseDeveloperExceptionPage();
else
		app.UseHsts();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
		ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
