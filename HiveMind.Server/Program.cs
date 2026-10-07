using FluentValidation;
using HiveMind.Server;
using HiveMind.Server.Endpoints;
using HiveMind.Server.HostedServices;
using HiveMind.Server.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register the DbContext service
builder.Services.AddDbContext<SqliteDBContext>(options => options.UseSqlite(SqliteDBContext.GetDataBaseConnectionString()));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<StationService>();
builder.Services.AddScoped<LibraryService>();
builder.Services.AddScoped<MediaItemService>();
builder.Services.AddScoped<ShowService>();
builder.Services.AddScoped<TagsService>();
builder.Services.AddScoped<QueryService>();
builder.Services.AddScoped<LineupService>();
builder.Services.AddScoped<SettingsService>();
builder.Services.AddScoped<ProgramStrategyService>();
builder.Services.AddScoped<BatchService>();
builder.Services.AddScoped<ProgramStrategyLineupService>();
builder.Services.AddScoped<TransitionTemplateService>();
builder.Services.AddScoped<ScheduleService>();
builder.Services.AddScoped<DroneService>();
builder.Services.AddScoped<ProgramEventService>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddHostedService<SchedulingBackgroundService>();
builder.Services.AddHostedService<MediaImporterBackgroundService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<SqliteDBContext>();
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