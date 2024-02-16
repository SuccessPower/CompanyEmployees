using CompanyEmployees.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();


builder.Services.AddControllers();

var app = builder.Build();

if (app.environment.IsDevelopment())
		app.UseDeveloperExceptionPage();
else
		app.UseHsts();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new FoarwardedHeadersOptions
{
		ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
