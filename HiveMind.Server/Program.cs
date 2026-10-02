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

builder.Services.AddTransient<StationService>();
builder.Services.AddTransient<LibraryService>();
builder.Services.AddTransient<MediaItemService>();
builder.Services.AddTransient<ShowService>();
builder.Services.AddTransient<TagsService>();
builder.Services.AddTransient<QueryService>();
builder.Services.AddTransient<LineupService>();
builder.Services.AddTransient<SettingsService>();
builder.Services.AddTransient<ProgramStrategyService>();
builder.Services.AddTransient<BatchService>();
builder.Services.AddTransient<ProgramStrategyLineupService>();
builder.Services.AddTransient<TransitionTemplateService>();
builder.Services.AddTransient<ScheduleService>();
builder.Services.AddTransient<DroneService>();
builder.Services.AddTransient<ProgramEventService>();

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