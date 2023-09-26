using Data;
using Data.Dtos;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using RegionsAPI.Extensions;
using Serilog;
using WebFramework.Extensions;
using WebFramework.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("SqlServer");

    options.UseSqlServer(connectionString);
});
builder.Services.InitializeAutoMapper();

builder.Services.AddSingleton<CacheService<RegionDto, Region>>();
builder.Services.AddSingleton<CacheService<EmployeeDto, Employee>>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog(logger);
});

builder.Services.AddHostedService<SaveBackgroundService>();

var app = builder.Build();

app.Services.SeedData(builder.Services, logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
