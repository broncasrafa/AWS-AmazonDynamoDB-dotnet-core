using AmazonDynamoDB.Api.Extensions;
using AmazonDynamoDB.Infrastructure.ServicesCollectionRegistration;


var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environments.Development}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.
builder.Services.AddSwaggerOpenAPI();
builder.Services.AddControllerAndJsonConfigurations();
builder.Services.AddSettingsConfigurations(configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAWSServices(configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();


// Configure the HTTP request pipeline.
var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCorsConfigure();
app.UseSwaggerConfigure();
app.UseCustomApiErrorsExceptionConfigure();
app.UseHealthChecks("/health");
app.UseStaticFiles();
app.MapControllers();

app.Run();

public partial class Program { }