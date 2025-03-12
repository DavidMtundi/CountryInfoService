using CountryInfoService.Core.Interfaces;
using CountryInfoService.Core.Services;
using CountryInfoService.Data;
using CountryInfoService.Infrastructure.Clients;
using CountryInfoService.Infrastructure.Repositories;
using CountryInfoService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceReference;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add automapper
builder.Services.AddAutoMapper(typeof(Program));

// Postgres Db
builder.Services.AddDbContext<AppDbContext>(options=>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the services
builder.Services.AddScoped<IRepository<Country>, CountryRepository>();
builder.Services.AddScoped<ICountryService, CountryService>();

// Register the soap Client
builder.Services.AddScoped<ICountryInfoSoapClient, CountryInfoSoapClient>();
builder.Services.AddScoped<CountryInfoServiceSoapTypeClient>(provider => 
{
    var client = new CountryInfoServiceSoapTypeClient(
        CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap,
        "http://webservices.oorsprong.org/websamples.countryinfo/CountryInfoService.wso");
    return client;
});

// Register the controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();

app.Run();
