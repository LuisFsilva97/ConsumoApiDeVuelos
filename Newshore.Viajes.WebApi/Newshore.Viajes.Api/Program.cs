using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newshore.Viajes.Api.Middleware;
using Newshore.Viajes.Application.ApplicationService;
using Newshore.Viajes.Application.IApplicationService;
using Newshore.Viajes.Business.IServices;
using Newshore.Viajes.Business.Services;
using Newshore.Viajes.Communications.IServices;
using Newshore.Viajes.Communications.Services;
using Newshore.Viajes.Repository;
using Newshore.Viajes.Repository.Data;
using Newshore.Viajes.Repository.IServices;
using Newshore.Viajes.Repository.Services;
using Serilog;
using System.Reflection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
try
{
    logger.Information("Application is starting");

    // Add services to the container.

    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    
    AddSwagger();

    // Agregar Servicios al contenedor de dependencias
    builder.Services.AddTransient<IApiFlightsService, ApiFlightsService>();
    builder.Services.Decorate<IApiFlightsService, CachedApiFlightsService>();
    builder.Services.AddTransient<ISearchFlightService, SearchFlightService>();
    builder.Services.AddTransient<ISearchFlightApplicationService, SearchFlightApplicationService>();
    builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    builder.Services.AddTransient<ISearchHistoryRespository, SearchHistoryRespository>();
    // Database
    builder.Services.AddDbContext<AppDbContext>
        (o => o.UseInMemoryDatabase("NewshoreFlights"));

    // In-Memory Caching
    builder.Services.AddMemoryCache();

    // builder.Services.AddSwaggerGen();

    // Serilog
    builder.Host.ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.SetMinimumLevel(LogLevel.Warning);
    }).UseSerilog((HostBuilderContext context, LoggerConfiguration loggerConfiguration) =>
    {
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    });
    builder.Services.AddCors(opt =>
    {
        opt.AddPolicy("Default_CorsPolicy", o =>
        {
            o.AllowAnyHeader();
            o.AllowAnyMethod();
            o.AllowAnyOrigin();
        });
    });
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseCors("Default_CorsPolicy");

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseMiddleware<ExceptionHandlerMiddleware>();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, ex.Message);
}

void AddSwagger()
{
    builder.Services.AddSwaggerGen(config =>
    {
        config.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Newshore.Viajes",
            Contact = new OpenApiContact
            {
                Name = "Gustavo Moreno",
                Email = "gustavoamoreno@outlook.com",
                Url = new Uri("https://github.com/gamorenoo/")
            }
        });
        //config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //{
        //    Description = @"Authorization header using the Bearer scheme. \r\n\r\n 
        //              Enter 'Bearer' [space] and then your token in the text input below.
        //              \r\n\r\n Example: 'Bearer 8c60e037-2722-4c50-a542-4df4f9ff1b26'",
        //    Name = "Bearer",
        //    In = ParameterLocation.Header,
        //    Type = SecuritySchemeType.Http,
        //    Scheme = "Bearer"
        //});
        //config.AddSecurityRequirement(new OpenApiSecurityRequirement()
        //{
        //    {
        //        new OpenApiSecurityScheme
        //        {
        //            Reference = new OpenApiReference
        //            {
        //                Type = ReferenceType.SecurityScheme,
        //                Id = "Bearer"
        //            },
        //            Scheme = "Http",
        //            Name = "Bearer",
        //            In = ParameterLocation.Header,
        //        },
        //        new List<string>()
        //    }
        //});
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
}
