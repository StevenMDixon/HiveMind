using FluentValidation;
using HiveMind.Server;
using HiveMind.Server.Endpoints;
using HiveMind.Server.HostedServices;
using HiveMind.Server.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register the DbContext service
builder.Services.AddDbContext<sqliteDBContext>(options => options.UseSqlite(sqliteDBContext.GetDataBaseConnectionString()));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddTransient<ChannelService>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

//builder.Services.AddHostedService<TimedHostedService>();
//builder.Services.AddHostedService<SchedulingBackgroundService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<sqliteDBContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

EndPointMapper.Map(app);

app.MapFallbackToFile("/index.html");

app.Run();