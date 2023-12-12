using System.Globalization;
using ForexTradingBotWebAPI.Database;
using ForexTradingBotWebAPI.Persistence.User;
using ForexTradingBotWebAPI.Service.User;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<PlatformDBContext>(options =>
    options.UseSqlServer(connection, sqlServerOptionsAction =>
    {
        sqlServerOptionsAction.EnableRetryOnFailure(
            maxRetryCount: 5,  // Adjust the maximum number of retries as needed
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));

builder.Services.AddDbContext<ApplicationUserDbContext>(options =>
    options.UseSqlServer(connection, sqlServerOptionsAction =>
    {
        sqlServerOptionsAction.EnableRetryOnFailure(
            maxRetryCount: 5,  // Adjust the maximum number of retries as needed
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddScoped<IUserRepo>(provider =>
    new UserRepo(
        provider.GetRequiredService<PlatformDBContext>(),
        provider.GetRequiredService<ApplicationUserDbContext>()
    ));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Set the default culture
var cultureInfo = new CultureInfo("en-US");
var requestLocalizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(cultureInfo),
    SupportedCultures = new List<CultureInfo> { cultureInfo },
    SupportedUICultures = new List<CultureInfo> { cultureInfo }
};

app.UseRequestLocalization(requestLocalizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Add this line to include the UseRouting middleware
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();