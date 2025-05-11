using System.Text;
using MediStoS.Database.DatabaseContext;
using MediStoS.Database.Repository.BatchRepository;
using MediStoS.Database.Repository.MedicineRepository;
using MediStoS.Database.Repository.SensorRepository;
using MediStoS.Database.Repository.StorageViolationRepository;
using MediStoS.Database.Repository.UserRepository;
using MediStoS.Database.Repository.WarehouseRepository;
using MediStoS.Services.SMTPService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Formatting.Json;
using Microsoft.Identity.Web;
using MediStoS.Extensions;
using MediStoS.Middleware;
using MediStoS.Database.Seeders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Logging.AddConsole().AddDebug();
// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
});



builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MediStoS", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter the token into header:"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string [] {}
        }
    });
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(new JsonFormatter(), "logs/logs.json", rollingInterval: RollingInterval.Month).CreateLogger();

AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapSwagger();
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<ApplicationDbContext>();
    //var medicines = MedicineSeeder.GetSeedMedicines();
    //await context.Medicines.AddRangeAsync(medicines);

    //var warehouses = WarehouseSeeder.GetSeedWarehouses();
    //await context.Warehouses.AddRangeAsync(warehouses);

    //var warehouseIds = await context.Warehouses.Select(a => a.Id).ToListAsync();
    //var medicineIds = await context.Medicines.Select(a => a.Id).ToListAsync();
    //var batches = BatchSeeder.GetSeedBatches(warehouseIds.ToArray(), medicineIds.ToArray());
    //await context.Batches.AddRangeAsync(batches);

    //var sensors = SensorSeeder.GetSeedSensors(warehouseIds.ToArray());
    //await context.Sensors.AddRangeAsync(sensors);

    //await context.SaveChangesAsync();
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during seeding");
}

app.Run();
