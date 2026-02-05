using Application;
using Microsoft.EntityFrameworkCore;
using Persistence.db;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors();


builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly);
    cfg.LicenseKey = builder.Configuration["MediatR:LicenseKey"];

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000", "https://localhost:3000"));

app.MapControllers();

// Create scope for my running services
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    // Get the connection to the DB
    var context = services.GetRequiredService<AppDbContext>();
    // Create the DB if doesn't exist yet and run any pending migrations 
    await context.Database.MigrateAsync();
    // Seed the DB
    await DbInitializer.SeedData(context);
}
catch (Exception ex)
{

    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during DB creation or migration");
}



app.Run();
